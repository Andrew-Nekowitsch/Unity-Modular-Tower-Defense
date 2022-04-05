using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{
    TileType type { get; set; }
    bool visible { get; set; }
    int x { get; set; }
    int y { get; set; }
    Neighbors neighbors { get; set; }
    DirectionType directionHome { get; set; }

    void Initialize(int x, int y);
    void SetCoordinates(int _x, int _y);
}
