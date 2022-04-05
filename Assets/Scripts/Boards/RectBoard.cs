
using UnityEngine;

public class RectBoard : IBoard
{
    public ITile[,] tiles { get; set; }
    public ITile startingTile { get; set; }
    public ITile currentTile { get; set; }
    public int width { get; set; }
    public int height { get; set; }

    public RectBoard(int x = 20, int y = 20)
    {
        width = x + 2;
        height = y + 2;

        InitializeTiles();
        InitializeBorders();
        InitializeStartingLocation();

        Log();
    }

    public void InitializeTiles()
    {
        tiles = new Tile[width, height];
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                Add(new Tile(x, y, TileType.Unknown));
            }
        }
    }

    public void InitializeBorders()
    {
        for (int w = 0; w < width; w++)
        {
            Add(new Tile(w, 0, TileType.Border));
            Add(new Tile(w, height - 1, TileType.Border));
        }
        for (int h = 0; h < height; h++)
        {
            Add(new Tile(0, h, TileType.Border));
            Add(new Tile(width - 1, h, TileType.Border));
        }
    }

    public void InitializeStartingLocation()
    {
        int x = Random.Range(1, width - 1);
        int y = Random.Range(1, height - 1);
        startingTile = new Tile(x, y, TileType.Start);
        Add(startingTile);

        currentTile = startingTile;
    }

    public ITile GetTileAt(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return null;
        return tiles[x, y];
    }

    public ITile NorthOf(ITile t)
    {
        return GetTileAt(t.x, t.y + 1);
    }

    public ITile EastOf(ITile t)
    {
        return GetTileAt(t.x + 1, t.y);
    }

    public ITile SouthOf(ITile t)
    {
        return GetTileAt(t.x, t.y - 1);
    }

    public ITile WestOf(ITile t)
    {
        return GetTileAt(t.x - 1, t.y);
    }

    public void ShiftCurrentTile(DirectionType dir)
    {
        switch (dir)
        {
            case DirectionType.Up:
                this.currentTile = NorthOf(this.currentTile);
                break;
            case DirectionType.Right:
                this.currentTile = EastOf(this.currentTile);
                break;
            case DirectionType.Down:
                this.currentTile = SouthOf(this.currentTile);
                break;
            case DirectionType.Left:
                this.currentTile = WestOf(this.currentTile);
                break;
            default:
                break;
        }
    }

    public void Add(ITile t)
    {
        if (t.x < 0 || t.x >= width || t.y < 0 || t.y >= height)
            return;
        SetNeighbors(t);
        tiles[t.x, t.y] = t;
    }

    public void SetNeighbors(ITile t)
    {
        t.neighbors.north = NorthOf(t);
        t.neighbors.south = SouthOf(t);
        t.neighbors.east = EastOf(t);
        t.neighbors.west = WestOf(t);
    }

    public void Log()
    {
        string str = "";

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (GetTileAt(x, y) == null)
                    str += "U\t";
                else
                    str += GetTileAt(x, y).type.ToString().Substring(0, 1).ToUpper() + "\t";
            }
            str += "\n";
        }
        Debug.Log(str);
    }
}