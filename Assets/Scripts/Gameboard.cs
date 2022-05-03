using UnityEngine;

public class Gameboard : MonoBehaviour
{
	public IBoard board;
	public ModuleSelector selector;
	public int width = 20;
	public int height = 20;
	public const int MODULE_SIZE = 10;
	public const int NUM_TILES_PER_MODULE = 5;

	public GameObject prefab_Unknown;
	public GameObject prefab_Start;
	public GameObject prefab_Path;
	public GameObject prefab_Border;
	public GameObject prefab_Obstacle;
	public GameObject prefab_End;


	private void Start()
	{
		InstantiateHierarchy();
		InitializeBoard();
	}

	private void InstantiateHierarchy()
	{
		foreach (ModuleType t in System.Enum.GetValues(typeof(ModuleType)))
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
		InstantiateSelector(new Vector3(board.StartingModule.X * MODULE_SIZE, board.StartingModule.Y * MODULE_SIZE, 0));
	}

	public void Setup()
	{
		board = new RectBoard
		{
			Modules = new Module[width, height]
		};
		InitializeBorders();
		InitializeInsideTiles();
		InitializeStartingLocation();
	}

	public void InitializeBorders()
	{
		for (int x = 0; x < width; x++)
		{
			board.AddHidden(new Module(x, 0, ModuleType.Border), GetPrefab(ModuleType.Border));
			board.AddHidden(new Module(x, height - 1, ModuleType.Border), GetPrefab(ModuleType.Border));
		}
		for (int y = 1; y < height - 1; y++)
		{
			board.AddHidden(new Module(0, y, ModuleType.Border), GetPrefab(ModuleType.Border));
			board.AddHidden(new Module(width - 1, y, ModuleType.Border), GetPrefab(ModuleType.Border));
		}
	}

	public void InitializeInsideTiles()
	{
		for (int x = 1; x < width - 1; x++)
		{
			for (int y = 1; y < height - 1; y++)
			{
				board.AddWithoutInstantiation(new Module(x, y, ModuleType.Unknown));
			}
		}
	}

	public void InitializeStartingLocation()
	{
		int x = Random.Range(1, width - 1);
		int y = Random.Range(1, height - 1);
		board.StartingModule = board.GetModuleAt(x, y);
		board.StartingModule.SetModuleType(ModuleType.Start);
		board.CurrentModule = board.StartingModule;
		board.CurrentModule.SetVisible(Visibility.Visible);
		board.InstantiateModule(board.CurrentModule, GetPrefab(ModuleType.Start));
	}

	public void InstantiateSelector(Vector3 pos)
	{
		selector.SetPos(pos);
	}

	private GameObject GetPrefab(ModuleType type)
	{
		GameObject prefab = prefab_Unknown;
		switch (type)
		{
			case (ModuleType.Border):
				prefab = prefab_Border;
				break;
			case (ModuleType.Start):
				prefab = prefab_Start;
				break;
			case (ModuleType.End):
				prefab = prefab_End;
				break;
			case (ModuleType.Path):
				prefab = prefab_Path;
				break;
			case (ModuleType.Obstacle):
				prefab = prefab_Obstacle;
				break;
			case (ModuleType.Unknown):
			default:
				break;
		}

		return prefab;
	}

	public void Research(DirectionType dir)
	{
		if (board.CurrentModule.GetModuleType() != ModuleType.Path && board.CurrentModule.GetModuleType() != ModuleType.Start)
			return;

		if (dir == DirectionType.Up)
		{
			ResearchModule(board.NorthOf(board.CurrentModule), DirectionType.Up);
			board.NorthOf(board.CurrentModule).Tiles?.SetDown();
			board.CurrentModule.Tiles?.SetUp();
		}
		else if (dir == DirectionType.Down)
		{
			ResearchModule(board.SouthOf(board.CurrentModule), DirectionType.Down);
			board.SouthOf(board.CurrentModule).Tiles?.SetUp();
			board.CurrentModule.Tiles?.SetDown();
		}
		else if (dir == DirectionType.Right)
		{
			ResearchModule(board.EastOf(board.CurrentModule), DirectionType.Right);
			board.EastOf(board.CurrentModule).Tiles?.SetLeft();
			board.CurrentModule.Tiles?.SetRight();
		}
		else if (dir == DirectionType.Left)
		{
			ResearchModule(board.WestOf(board.CurrentModule), DirectionType.Left);
			board.WestOf(board.CurrentModule).Tiles?.SetRight();
			board.CurrentModule.Tiles?.SetLeft();
		}
	}

	private void ResearchModule(IModule module, DirectionType dir)
	{
		if (module == null)
			return;
		if (module.GetVisible() == Visibility.Visible)
			return;
		if (module.GetModuleType() != ModuleType.Border)
		{
			module = board.GetModuleAt(module.X, module.Y);
			module.SetModuleType(GenerateTileType());
		}
		board.Research(module, GetPrefab(module.GetModuleType()));
	}

	// TODO: make variable odds based on currently placed tiles
	public ModuleType GenerateTileType()
	{
		ModuleType type;
		int num = Random.Range(0, 100);
		if (num >= 20) // 80%
		{
			type = ModuleType.Path;
		}
		else if (num >= 5) // 15%
		{
			type = ModuleType.Obstacle;
		}
		else // 5%
		{
			type = ModuleType.End;
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