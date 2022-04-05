using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    private GameObject selector;
    public GameObject prefab_Selector;

    private float x;
    private float y;

    public void InstantiateSelector(Transform t, Vector3 pos)
    {
        selector = GameObject.Instantiate(prefab_Selector, t);
        selector.transform.position = pos;
        x = pos.x;
        y = pos.y;
    }

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
        selector.transform.position = new Vector3(x, y, 0);
    }
}
