using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{

	public static Gameboard Instance { get; set; }

	public IBoard board;
	public TileSelector selector;
	public int width = 20;
	public int height = 20;
	public const int TILE_SIZE = 10;
	public const int NUM_SECTIONS_PER_TILE = 5;

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
		InitializeBoard();
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

	public void InitializeBoard()
	{
		Setup();
		InstantiateSelector(gameObject.transform, new Vector3(board.StartingTile.X * TILE_SIZE, board.StartingTile.Y * TILE_SIZE, 0));
	}

	public void Setup()
	{
		board = new RectBoard
		{
			Tiles = new Tile[width, height]
		};
		InitializeBorders();
		InitializeInsideTiles();
		InitializeStartingLocation();
	}

	public void InitializeBorders()
	{
		for (int x = 0; x < width; x++)
		{
			board.AddHidden(new Tile(x, 0, TileType.Border), GetPrefab(TileType.Border));
			board.AddHidden(new Tile(x, height - 1, TileType.Border), GetPrefab(TileType.Border));
		}
		for (int y = 1; y < height - 1; y++)
		{
			board.AddHidden(new Tile(0, y, TileType.Border), GetPrefab(TileType.Border));
			board.AddHidden(new Tile(width - 1, y, TileType.Border), GetPrefab(TileType.Border));
		}
	}

	public void InitializeInsideTiles()
	{
		for (int x = 1; x < width - 1; x++)
		{
			for (int y = 1; y < height - 1; y++)
			{
				board.AddWithoutInstantiation(new Tile(x, y, TileType.Unknown));
			}
		}

		board.SetNeighbors();
	}

	public void InitializeStartingLocation()
	{
		int x = Random.Range(1, width - 1);
		int y = Random.Range(1, height - 1);
		board.StartingTile = board.GetTileAt(x, y);
		board.StartingTile.SetTileType(TileType.Start);
		board.CurrentTile = board.StartingTile;
		board.CurrentTile.SetVisible(Visibility.Visible);
		board.InstantiateTile(board.CurrentTile, GetPrefab(TileType.Start));
	}

	public void InstantiateSelector(Transform t, Vector3 pos)
	{
		GameObject go = GameObject.Instantiate(prefab_Selector, t);
		selector = go.GetComponent<TileSelector>();
		go.transform.position = pos;
		selector.x = pos.x;
		selector.y = pos.y;
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
		if (board.CurrentTile.GetTileType() != TileType.Path && board.CurrentTile.GetTileType() != TileType.Start)
			return;

		if (dir == DirectionType.Up)
		{
			ResearchTile(board.NorthOf(board.CurrentTile));
		}
		else if (dir == DirectionType.Down)
		{
			ResearchTile(board.SouthOf(board.CurrentTile));
		}
		else if (dir == DirectionType.Right)
		{
			ResearchTile(board.EastOf(board.CurrentTile));
		}
		else if (dir == DirectionType.Left)
		{
			ResearchTile(board.WestOf(board.CurrentTile));
		}
	}

	private void ResearchTile(ITile tile)
	{
		if (tile.GetVisible() == Visibility.Visible)
			return;
		if (tile.GetTileType() != TileType.Border)
		{
			tile = board.GetTileAt(tile.X, tile.Y);
			tile.SetTileType(GenerateTileType());
		}
		board.Research(tile, GetPrefab(tile.GetTileType()));
	}

	// TODO: make variable odds based on currently placed tiles
	private TileType GenerateTileType()
	{
		TileType type;
		int num = Random.Range(0, 100);
		if (num >= 20) // 80%
		{
			type = TileType.Path;
		}
		else if (num >= 5) // 15%
		{
			type = TileType.Obstacle;
		}
		else // 5%
		{
			type = TileType.End;
		}

		return type;
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