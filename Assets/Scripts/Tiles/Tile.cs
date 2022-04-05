using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Tile : ITile
{
    public TileType type { get; set; }
    public bool visible { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public Neighbors neighbors { get; set; }
    public DirectionType directionHome { get; set; }

    public Tile(int x, int y)
    {
        this.visible = false;
        this.x = x;
        this.y = y;
        Initialize(x, y);
    }
    public Tile(int x, int y, TileType type)
    {
        this.visible = false;
        this.x = x;
        this.y = y;
        this.type = type;
        Initialize(x, y);
    }

    public void Initialize(int x, int y)
    {
        this.neighbors = new Neighbors();
        SetCoordinates(x, y);
    }

    public void SetCoordinates(int _x, int _y)
    {
        this.x = _x;
        this.y = _y;
    }

    public void AddNeightbor(DirectionType dir)
    {

    }
}