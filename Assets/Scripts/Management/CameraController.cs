using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;

    private float x;
    private float y;
    private float z;

    private void Awake()
    {
        if (cam == null)
        {
            //cam = GameObject.FindObjectOfType<Camera>();
            cam = Camera.main;
        }

        x = cam.transform.position.x;
        y = cam.transform.position.y;
        z = cam.transform.position.z;
    }

    public void MoveCamera(DirectionType dir)
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
            default:
                break;
        }
        cam.transform.position = new Vector3(x, y, z);
    }
}
