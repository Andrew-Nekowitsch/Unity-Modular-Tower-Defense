using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleSelector : MonoBehaviour
{
    public float x;
    public float y;

    public void ShiftSelector(DirectionType dir)
    {
        switch (dir)
        {
            case DirectionType.Up:
                y += Gameboard.MODULE_SIZE;
                break;
            case DirectionType.Right:
                x += Gameboard.MODULE_SIZE;
                break;
            case DirectionType.Down:
                y -= Gameboard.MODULE_SIZE;
                break;
            case DirectionType.Left:
                x -= Gameboard.MODULE_SIZE;
                break;
        }
        gameObject.transform.position = new Vector3(x, y, 0);
    }
}
