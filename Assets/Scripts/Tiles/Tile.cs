class Tile : ITile
{
    public TileType Type { get; set; }
    public bool Visible { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Neighbors Neighbors { get; set; }
    public DirectionType DirectionHome { get; set; }

    public Section[,] sections {get; set;}

    public Tile(int x, int y, int numSections = 5)
    {
        sections = new Section[numSections, numSections];
        Initialize(x, y);
    }
    public Tile(int x, int y, TileType type, int numSections = 5)
    {
        this.Type = type;
        sections = new Section[numSections, numSections];
        Initialize(x, y);
    }

    public void Initialize(int x, int y)
    {
        this.X = x;
        this.Y = y;
        Visible = false;
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
        return Type.ToString();
	}
}