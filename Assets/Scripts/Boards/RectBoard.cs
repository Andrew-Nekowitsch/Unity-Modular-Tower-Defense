
using UnityEngine;

public class RectBoard : IBoard
{
	public ITile[,] Tiles { get; set; }
	public ITile StartingTile { get; set; }
	public ITile CurrentTile { get; set; }
	private readonly Gameboard gb = Singleton.Instance.Gameboard;

	public void InstantiateTile(ITile tile, GameObject prefab)
	{
		tile.Instantiate(prefab, GameObject.Find(tile.ToString()));

		if (tile.GetTileType() == TileType.Border && tile.GetVisible() == Visibility.Unknown)
		{
			tile.SetVisibility(Visibility.Visible);
			return;
		}

		ITile[] neighbors = { NorthOf(tile), EastOf(tile), SouthOf(tile), WestOf(tile) };
		foreach (ITile tileRef in neighbors)
		{
			if (tileRef == null) continue;
			if (tileRef.GetTileType() == TileType.Unknown || (tileRef.GetTileType() == TileType.Border && tileRef.GetVisible() == Visibility.Hidden))
			{
				tileRef.Instantiate(gb.prefab_Unknown, GameObject.Find(tileRef.ToString()));
				tile.SetVisibility(Visibility.Unknown);
			}
		}
	}

	public void InstantiateBorder(ITile tile)
	{
		GameObject objParentInHierarchy = GameObject.Find(tile.ToString());
		tile.Instantiate(gb.prefab_Border, objParentInHierarchy);
		tile.SetVisibility(Visibility.Hidden);
	}

	public ITile GetTileAt(int x, int y)
	{
		if (InvalidTileLocation(x, y))
			return null;
		return Tiles[x, y];
	}

	public ITile NorthOf(ITile t) => GetTileAt(t.X, t.Y + 1);

	public ITile EastOf(ITile t) => GetTileAt(t.X + 1, t.Y);

	public ITile SouthOf(ITile t) => GetTileAt(t.X, t.Y - 1);

	public ITile WestOf(ITile t) => GetTileAt(t.X - 1, t.Y);

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

	public bool AddHidden(ITile t, GameObject prefab)
	{
		if (AddWithoutInstantiation(t))
		{
			t.SetVisible(Visibility.Hidden);
			InstantiateBorder(Tiles[t.X, t.Y]);
			return true;
		}
		return false;
	}

	public bool Add(ITile t, GameObject prefab)
	{
		if (AddWithoutNeighbors(t, prefab))
		{
			SetNeighbors(t);
			return true;
		}
		return false;
	}

	public bool AddWithoutNeighbors(ITile t, GameObject prefab)
	{
		if (AddWithoutInstantiation(t))
		{
			InstantiateTile(Tiles[t.X, t.Y], prefab);
			return true;
		}
		return false;
	}

	public bool AddWithoutInstantiation(ITile t)
	{
		if (InvalidTileLocation(t))
			return false;
		Tiles[t.X, t.Y] = t;
		return true;
	}

	public void Research(ITile t, GameObject prefab)
	{
		Add(t, prefab);
	}

	public void SetNeighbors()
	{
		for (int x = 0; x < gb.width; x++)
		{
			for (int y = 0; y < gb.height; y++)
			{
				SetNeighbors(Tiles[x, y]);
			}
		}
	}

	public void SetNeighbors(ITile t) => t.SetNeighbors(NorthOf(t), SouthOf(t), EastOf(t), WestOf(t));

	public bool InvalidTileLocation(ITile t) => InvalidTileLocation(t.X, t.Y);

	public bool InvalidTileLocation(int x, int y) => x < 0 || x >= gb.width || y < 0 || y >= gb.height;

	public void Log()
	{
		string str = "";

		for (int y = 0; y < gb.height; y++)
		{
			for (int x = 0; x < gb.width; x++)
			{
				if (GetTileAt(x, y) == null)
					str += "null??\t";
				else
					str += GetTileAt(x, y).GetTileType().ToString().Substring(0, 1).ToUpper() + "\t";
			}
			str += "\n";
		}
		Debug.Log(str);
	}
}