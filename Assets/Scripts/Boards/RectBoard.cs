
using UnityEngine;

public class RectBoard : IBoard
{
	public IModule[,] Modules { get; set; }
	public IModule StartingModule { get; set; }
	public IModule CurrentModule { get; set; }
	private readonly Gameboard gb = Singleton.Instance.Gameboard;

	public void InstantiateModule(IModule module, GameObject prefab)
	{
		module.Instantiate(prefab, GameObject.Find(module.ToString()));

		if (module.GetModuleType() == ModuleType.Border && module.GetVisible() == Visibility.Unknown)
		{
			module.SetVisibility(Visibility.Visible);
			return;
		}

		IModule[] neighbors = { NorthOf(module), EastOf(module), SouthOf(module), WestOf(module) };
		foreach (IModule moduleRef in neighbors)
		{
			if (moduleRef == null) continue;
			if (moduleRef.GetModuleType() == ModuleType.Unknown || (moduleRef.GetModuleType() == ModuleType.Border && moduleRef.GetVisible() == Visibility.Hidden))
			{
				moduleRef.Instantiate(gb.prefab_Unknown, GameObject.Find(moduleRef.ToString()));
				module.SetVisibility(Visibility.Unknown);
			}
		}
	}

	public void InstantiateBorder(IModule tile)
	{
		GameObject objParentInHierarchy = GameObject.Find(tile.ToString());
		tile.Instantiate(gb.prefab_Border, objParentInHierarchy);
		tile.SetVisibility(Visibility.Hidden);
	}

	public IModule GetModuleAt(int x, int y)
	{
		if (InvalidTileLocation(x, y))
			return null;
		return Modules[x, y];
	}

	public IModule NorthOf(IModule t) => GetModuleAt(t.X, t.Y + 1);

	public IModule EastOf(IModule t) => GetModuleAt(t.X + 1, t.Y);

	public IModule SouthOf(IModule t) => GetModuleAt(t.X, t.Y - 1);

	public IModule WestOf(IModule t) => GetModuleAt(t.X - 1, t.Y);

	public void ShiftCurrentTile(DirectionType dir)
	{
		switch (dir)
		{
			case DirectionType.Up:
				this.CurrentModule = NorthOf(this.CurrentModule);
				break;
			case DirectionType.Right:
				this.CurrentModule = EastOf(this.CurrentModule);
				break;
			case DirectionType.Down:
				this.CurrentModule = SouthOf(this.CurrentModule);
				break;
			case DirectionType.Left:
				this.CurrentModule = WestOf(this.CurrentModule);
				break;
			default:
				break;
		}
	}

	public bool AddHidden(IModule t, GameObject prefab)
	{
		if (AddWithoutInstantiation(t))
		{
			t.SetVisible(Visibility.Hidden);
			InstantiateBorder(Modules[t.X, t.Y]);
			return true;
		}
		return false;
	}

	public bool Add(IModule t, GameObject prefab)
	{
		if (AddWithoutInstantiation(t))
		{
			InstantiateModule(Modules[t.X, t.Y], prefab);
			return true;
		}
		return false;
	}

	public bool AddWithoutInstantiation(IModule t)
	{
		if (InvalidTileLocation(t))
			return false;
		Modules[t.X, t.Y] = t;
		return true;
	}

	public void Research(IModule t, GameObject prefab)
	{
		Add(t, prefab);
	}

	public bool InvalidTileLocation(IModule t) => InvalidTileLocation(t.X, t.Y);

	public bool InvalidTileLocation(int x, int y) => x < 0 || x >= gb.width || y < 0 || y >= gb.height;

	public void Log()
	{
		string str = "";

		for (int y = 0; y < gb.height; y++)
		{
			for (int x = 0; x < gb.width; x++)
			{
				if (GetModuleAt(x, y) == null)
					str += "null??\t";
				else
					str += GetModuleAt(x, y).GetModuleType().ToString().Substring(0, 1).ToUpper() + "\t";
			}
			str += "\n";
		}
		Debug.Log(str);
	}
}