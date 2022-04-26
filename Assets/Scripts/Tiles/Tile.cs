using UnityEngine;

public enum Visibility
{
	Hidden,
	Unknown,
	Visible
}

class Tile : ITile
{
	public int X { get; set; }
	public int Y { get; set; }
	public Neighbors Neighbors { get; set; }
	public DirectionType WalkDir { get; set; }
	public Section[,] Sections { get; set; }
	private Visibility visible;
	private GameObject gameObject;
	private TileType type;

	public Tile(TileType type, int numSections)
	{
		this.SetVisible(Visibility.Hidden);
		this.SetTileType(type);
		SetNumSections(numSections);
	}
	public Tile(int x, int y, int numSections = 5)
	{
		this.SetVisible(Visibility.Hidden);
		SetNumSections(numSections);
		Initialize(x, y);
	}
	public Tile(int x, int y, TileType type, int numSections = 5)
	{
		this.SetVisible(Visibility.Hidden);
		this.SetTileType(type);
		SetNumSections(numSections);
		Initialize(x, y);
	}

	public void Instantiate(GameObject prefab, GameObject parent)
	{
		this.SetGameObject(
			GameObject.Instantiate(prefab,
				new Vector3(X * Gameboard.TILE_SIZE, Y * Gameboard.TILE_SIZE, 0),
				Quaternion.identity, parent.transform)
				);
		this.GetGameObject().name = "[" + X + ", " + Y + "]";
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

	public void SetNeighbors(ITile n, ITile e, ITile s, ITile w)
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

	public TileType GetTileType()
	{
		return type;
	}

	public void SetTileType(TileType value)
	{
		if (type != TileType.Unknown)
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

	public void SetNumSections(int num)
	{
		Sections = new Section[num, num];
	}
}