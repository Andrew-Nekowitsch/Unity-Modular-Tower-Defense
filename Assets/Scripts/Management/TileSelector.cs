using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public float x;
    public float y;

    public void ShiftSelector(DirectionType dir)
    {
        switch (dir)
        {
            case DirectionType.Up:
                y += Gameboard.TILE_SIZE;
                break;
            case DirectionType.Right:
                x += Gameboard.TILE_SIZE;
                break;
            case DirectionType.Down:
                y -= Gameboard.TILE_SIZE;
                break;
            case DirectionType.Left:
                x -= Gameboard.TILE_SIZE;
                break;
        }
        gameObject.transform.position = new Vector3(x, y, 0);
    }
}
