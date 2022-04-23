
using UnityEngine;

public class RectBoard : IBoard
{
	public ITile[,] Tiles { get; set; }
	public ITile StartingTile { get; set; }
	public ITile CurrentTile { get; set; }
	public int Width { get; set; }
	public int Height { get; set; }

	public RectBoard(int x = 20, int y = 20)
	{
		Width = x + 2;
		Height = y + 2;

		Setup();

		Log();
	}

	public void Setup()
	{
		Tiles = new Tile[Width, Height];

		InitializeInsideTiles();
		InitializeBorders();
		InitializeStartingLocation();
		SetNeighbors();
	}

	public void InitializeInsideTiles()
	{
		for (int x = 1; x < Width - 1; x++)
		{
			for (int y = 1; y < Height - 1; y++)
			{
				AddWithoutNeighbors(new Tile(x, y, TileType.Unknown));
			}
		}
	}

	public void InitializeBorders()
	{
		for (int w = 0; w < Width; w++)
		{
			AddWithoutNeighbors(new Tile(w, 0, TileType.Border));
			AddWithoutNeighbors(new Tile(w, Height - 1, TileType.Border));
		}
		for (int h = 0; h < Height; h++)
		{
			AddWithoutNeighbors(new Tile(0, h, TileType.Border));
			AddWithoutNeighbors(new Tile(Width - 1, h, TileType.Border));
		}
	}

	public void InitializeStartingLocation()
	{
		int x = Random.Range(1, Width - 1);
		int y = Random.Range(1, Height - 1);
		StartingTile = new Tile(x, y, TileType.Start);
		AddWithoutNeighbors(StartingTile);

		CurrentTile = StartingTile;
	}

	public ITile GetTileAt(int x, int y)
	{
		if (x < 0 || x >= Width || y < 0 || y >= Height)
			return null;
		return Tiles[x, y];
	}

	public ITile NorthOf(ITile t)
	{
		return GetTileAt(t.X, t.Y + 1);
	}

	public ITile EastOf(ITile t)
	{
		return GetTileAt(t.X + 1, t.Y);
	}

	public ITile SouthOf(ITile t)
	{
		return GetTileAt(t.X, t.Y - 1);
	}

	public ITile WestOf(ITile t)
	{
		return GetTileAt(t.X - 1, t.Y);
	}

	public void ShiftCurrentTile(DirectionType dir)
	{
		switch (dir)
		{
			case DirectionType.Up:
				this.CurrentTile = NorthOf(this.CurrentTile);
				break;
			case DirectionType.Right:
				this.CurrentTile = EastOf(this.CurrentTile);
				break;
			case DirectionType.Down:
				this.CurrentTile = SouthOf(this.CurrentTile);
				break;
			case DirectionType.Left:
				this.CurrentTile = WestOf(this.CurrentTile);
				break;
			default:
				break;
		}
	}

	public void Add(ITile t)
	{
		if (AddWithoutNeighbors(t))
			SetNeighbors(t);
	}

	public bool AddWithoutNeighbors(ITile t)
	{
		if (t.X < 0 || t.X >= Width || t.Y < 0 || t.Y >= Height)
			return false;
		Tiles[t.X, t.Y] = t;
		return true;
	}

	public void SetNeighbors()
	{
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
			{
				SetNeighbors(Tiles[x, y]);
			}
		}
	}

	public void SetNeighbors(ITile t)
	{
		t.SetNeighbors(NorthOf(t), SouthOf(t), EastOf(t), WestOf(t));
	}

	public void Log()
	{
		string str = "";

		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				if (GetTileAt(x, y) == null)
					str += "null??\t";
				else
					str += GetTileAt(x, y).Type.ToString().Substring(0, 1).ToUpper() + "\t";
			}
			str += "\n";
		}
		Debug.Log(str);
	}
}