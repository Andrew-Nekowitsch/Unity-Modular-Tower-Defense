using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{
    TileType Type { get; set; }
    bool Visible { get; set; }
    int X { get; set; }
    int Y { get; set; }
    Neighbors Neighbors { get; set; }
    DirectionType DirectionHome { get; set; }

    void Initialize(int x, int y);
    void SetCoordinates(int _x, int _y);
    void SetNeighbors(ITile n, ITile e, ITile s, ITile w);
    string ToString();
}
