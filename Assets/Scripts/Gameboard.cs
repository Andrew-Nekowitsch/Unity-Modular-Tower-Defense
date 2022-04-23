using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{
	public IBoard board;
	public GameObject[,] boardObjects;
	public TileSelector selector;
	public int width = 20;
	public int height = 20;
	public const int TILE_SIZE = 10;

	public GameObject prefab_Unknown;
	public GameObject prefab_Start;
	public GameObject prefab_Path;
	public GameObject prefab_Border;
	public GameObject prefab_Obstacle;
	public GameObject prefab_End;
	public GameObject prefab_Selector;


	private void Awake()
	{
		InstantiateHierarchy();
		InstantiateBoard();
	}

	public void InstantiateTile(ITile tile)
	{
		tile.Visible = true;
		Instantiate(tile);

		if (tile.Type == TileType.Border)
			return;

		if (tile.Neighbors.north.Type == TileType.Unknown || tile.Neighbors.north.Type == TileType.Border)
		{
			Instantiate(new Tile(tile.X, tile.Y + 1, TileType.Unknown));
			tile.Neighbors.north.Visible = true;
		}
		if (tile.Neighbors.south.Type == TileType.Unknown || tile.Neighbors.south.Type == TileType.Border)
		{
			Instantiate(new Tile(tile.X, tile.Y - 1, TileType.Unknown));
			tile.Neighbors.south.Visible = true;
		}
		if (tile.Neighbors.east.Type == TileType.Unknown || tile.Neighbors.east.Type == TileType.Border)
		{
			Instantiate(new Tile(tile.X + 1, tile.Y, TileType.Unknown));
			tile.Neighbors.east.Visible = true;
		}
		if (tile.Neighbors.west.Type == TileType.Unknown || tile.Neighbors.west.Type == TileType.Border)
		{
			Instantiate(new Tile(tile.X - 1, tile.Y, TileType.Unknown));
			tile.Neighbors.west.Visible = true;
		}
	}

	private void InstantiateHierarchy()
	{
		foreach (TileType t in System.Enum.GetValues(typeof(TileType)))
		{
			GameObject objParentInHierarchy = GameObject.Find(t.ToString());
			if (objParentInHierarchy == null)
			{
				objParentInHierarchy = new GameObject(t.ToString());
				objParentInHierarchy.transform.parent = gameObject.transform;
			}
		}
	}

	public void InstantiateBoard()
	{
		board = new RectBoard(width, height);
		boardObjects = new GameObject[width + 2, height + 2];

		InstantiateTile(board.StartingTile);
		InstantiateSelector(gameObject.transform, new Vector3(board.StartingTile.X * TILE_SIZE, board.StartingTile.Y * TILE_SIZE, 0));
	}

	public void InstantiateSelector(Transform t, Vector3 pos)
	{
		GameObject go = GameObject.Instantiate(prefab_Selector, t);
		selector = go.GetComponent<TileSelector>();
		go.transform.position = pos;
		selector.x = pos.x;
		selector.y = pos.y;
	}

	private void Instantiate(ITile t)
	{
		GameObject objParentInHierarchy = GameObject.Find(t.ToString());
		GameObject objCoordsInHierarchy = new GameObject("[" + t.X + ", " + t.Y + "]");
		objCoordsInHierarchy.transform.parent = objParentInHierarchy.transform;

		if (boardObjects[t.X, t.Y] != null && board.Tiles[t.X, t.Y].Type == TileType.Unknown)
			Destroy(boardObjects[t.X, t.Y].transform.parent.gameObject);

		boardObjects[t.X, t.Y] = GameObject.Instantiate(GetPrefab(t.Type), new Vector3(t.X * TILE_SIZE, t.Y * TILE_SIZE, 0), Quaternion.identity, objCoordsInHierarchy.transform);

		if (t.Type != TileType.Unknown && t.Type != TileType.Start && t.Visible == false)
			boardObjects[t.X, t.Y].SetActive(false);
	}

	private GameObject GetPrefab(TileType type)
	{
		GameObject prefab = prefab_Unknown;
		switch (type)
		{
			case (TileType.Border):
				prefab = prefab_Border;
				break;
			case (TileType.Start):
				prefab = prefab_Start;
				break;
			case (TileType.End):
				prefab = prefab_End;
				break;
			case (TileType.Path):
				prefab = prefab_Path;
				break;
			case (TileType.Obstacle):
				prefab = prefab_Obstacle;
				break;
			case (TileType.Unknown):
			default:
				break;
		}

		return prefab;
	}

	public void Research(DirectionType dir)
	{
		if (board.CurrentTile.Type != TileType.Path && board.CurrentTile.Type != TileType.Start)
			return;

		if (dir == DirectionType.Up)
		{
			if (board.NorthOf(board.CurrentTile).Type != TileType.Unknown)
			{
				InstantiateTile(board.NorthOf(board.CurrentTile));
				return;
			}

			board.CurrentTile.Neighbors.north = GenerateTileType(board.CurrentTile.X, board.CurrentTile.Y + 1);
			board.Add(board.CurrentTile.Neighbors.north);
			InstantiateTile(board.CurrentTile.Neighbors.north);
		}
		else if (dir == DirectionType.Down)
		{
			if (board.SouthOf(board.CurrentTile).Type != TileType.Unknown)
			{
				InstantiateTile(board.SouthOf(board.CurrentTile));
				return;
			}

			board.CurrentTile.Neighbors.south = GenerateTileType(board.CurrentTile.X, board.CurrentTile.Y - 1);
			board.Add(board.CurrentTile.Neighbors.south);
			InstantiateTile(board.CurrentTile.Neighbors.south);
		}
		else if (dir == DirectionType.Right)
		{
			if (board.EastOf(board.CurrentTile).Type != TileType.Unknown)
			{
				InstantiateTile(board.EastOf(board.CurrentTile));
				return;
			}

			board.CurrentTile.Neighbors.east = GenerateTileType(board.CurrentTile.X + 1, board.CurrentTile.Y);
			board.Add(board.CurrentTile.Neighbors.east);
			InstantiateTile(board.CurrentTile.Neighbors.east);
		}
		else if (dir == DirectionType.Left)
		{
			if (board.WestOf(board.CurrentTile).Type != TileType.Unknown)
			{
				InstantiateTile(board.WestOf(board.CurrentTile));
				return;
			}

			board.CurrentTile.Neighbors.west = GenerateTileType(board.CurrentTile.X - 1, board.CurrentTile.Y);
			board.Add(board.CurrentTile.Neighbors.west);
			InstantiateTile(board.CurrentTile.Neighbors.west);
		}
	}

	private Tile GenerateTileType(int x, int y)
	{
		Tile t = new Tile(x, y);
		int num = Random.Range(0, 100);
		if (num >= 20) // 80%
		{
			t.Type = TileType.Path;
		}
		else if (num >= 5) // 15%
		{
			t.Type = TileType.Obstacle;
		}
		else // 5%
		{
			t.Type = TileType.End;
		}

		t.Visible = true;
		return t;
	}

	public void Up()
	{
		board.ShiftCurrentTile(DirectionType.Up);
		ShiftSelector(DirectionType.Up);
	}

	public void Down()
	{
		board.ShiftCurrentTile(DirectionType.Down);
		ShiftSelector(DirectionType.Down);
	}

	public void Left()
	{
		board.ShiftCurrentTile(DirectionType.Left);
		ShiftSelector(DirectionType.Left);
	}

	public void Right()
	{
		board.ShiftCurrentTile(DirectionType.Right);
		ShiftSelector(DirectionType.Right);
	}

	void ShiftSelector(DirectionType dir)
	{
		selector.ShiftSelector(dir);
	}
}