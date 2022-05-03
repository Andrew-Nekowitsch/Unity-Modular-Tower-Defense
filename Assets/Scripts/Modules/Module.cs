using UnityEngine;

class Module : IModule
{
	public int X { get; set; }
	public int Y { get; set; }
	public DirectionType WalkDir { get; set; }
	public Tiles Tiles { get; set; }
	private Visibility visible;
	private GameObject gameObject;
	private ModuleType type;

	public Module(ModuleType type)
	{
		this.SetVisible(Visibility.Hidden);
		this.SetModuleType(type);
	}
	public Module(int x, int y)
	{
		this.SetVisible(Visibility.Hidden);
		Initialize(x, y);
	}
	public Module(int x, int y, ModuleType type)
	{
		this.SetVisible(Visibility.Hidden);
		this.SetModuleType(type);
		Initialize(x, y);
	}

	public void Set(IModule m)
	{
		WalkDir = m.WalkDir;
		Tiles = m.Tiles;
		visible = m.GetVisible();
		type = m.GetModuleType();
	}

	public void Instantiate(GameObject prefab, GameObject parent)
	{
		this.SetGameObject(
			GameObject.Instantiate(prefab,
				new Vector3(X * Gameboard.MODULE_SIZE, Y * Gameboard.MODULE_SIZE, 0), Quaternion.identity, parent.transform));
		if (GetModuleType() == ModuleType.Path)
		{
			Tiles old = Tiles;
			Tiles = this.GetGameObject().GetComponent<Tiles>();
			if (old != null)
				Tiles.Initialize(old);
			else
				Tiles.Initialize();
		}
	}

	public void SetVisibility(Visibility v)
	{
		this.SetVisible(v);
		this.gameObject.SetActive(GetVisible() != Visibility.Hidden);
	}

	public void Initialize(int x, int y)
	{
		SetVisible(Visibility.Hidden);
		SetCoordinates(x, y);
	}

	public void SetCoordinates(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	public override string ToString()
	{
		return GetModuleType().ToString();
	}

	public ModuleType GetModuleType()
	{
		return type;
	}

	public void SetModuleType(ModuleType value)
	{
		if (type != ModuleType.Unknown)
			return;
		type = value;
	}

	public Visibility GetVisible()
	{
		return visible;
	}

	public void SetVisible(Visibility value)
	{
		visible = value;
	}

	public GameObject GetGameObject()
	{
		return gameObject;
	}

	public void SetGameObject(GameObject value)
	{
		if (this.gameObject != null)
			GameObject.Destroy(this.GetGameObject());
		gameObject = value;
		this.GetGameObject().name = "[" + X + ", " + Y + "]";
	}
}