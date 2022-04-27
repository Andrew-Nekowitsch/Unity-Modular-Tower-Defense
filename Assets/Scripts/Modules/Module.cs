using UnityEngine;

class Module : IModule
{
	public int X { get; set; }
	public int Y { get; set; }
	public Neighbors Neighbors { get; set; }
	public DirectionType WalkDir { get; set; }
	public Tiles Tiles { get; set; }
	private Visibility visible;
	private GameObject gameObject;
	private ModuleType type;

	public Module(ModuleType type)
	{
		this.SetVisible(Visibility.Hidden);
		this.SetTileType(type);
	}
	public Module(int x, int y)
	{
		this.SetVisible(Visibility.Hidden);
		Initialize(x, y);
	}
	public Module(int x, int y, ModuleType type)
	{
		this.SetVisible(Visibility.Hidden);
		this.SetTileType(type);
		Initialize(x, y);
	}

	public void Instantiate(GameObject prefab, GameObject parent)
	{
		this.SetGameObject(
			GameObject.Instantiate(prefab,
				new Vector3(X * Gameboard.MODULE_SIZE, Y * Gameboard.MODULE_SIZE, 0),
				Quaternion.identity, parent.transform)
				);
		this.GetGameObject().name = "[" + X + ", " + Y + "]";
		Tiles = this.GetGameObject().GetComponent<Tiles>();
	}

	public void SetVisibility(Visibility v)
	{
		this.SetVisible(v);
		this.gameObject.SetActive(GetVisible() != Visibility.Hidden);
	}

	public void Initialize(int x, int y)
	{
		SetVisible(Visibility.Hidden);
		Neighbors = new Neighbors();
		SetCoordinates(x, y);
	}

	public void SetCoordinates(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	public void SetNeighbors(IModule n, IModule e, IModule s, IModule w)
	{
		Neighbors.north = n;
		Neighbors.east = e;
		Neighbors.south = s;
		Neighbors.west = w;
	}

	public override string ToString()
	{
		return GetTileType().ToString();
	}

	public ModuleType GetTileType()
	{
		return type;
	}

	public void SetTileType(ModuleType value)
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
	}
}