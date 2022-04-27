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

        SetXYZ();
    }

    public void MoveCamera(DirectionType dir)
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
            default:
                break;
        }
        cam.transform.position = new Vector3(x, y, z);
    }

    private void SetXYZ()
    {
        x = cam.transform.position.x;
        y = cam.transform.position.y;
        z = cam.transform.position.z;
    }

    public void SetPosition(float x, float y, float z)
    {
        cam.transform.position = new Vector3(x, y, z);
        SetXYZ();
	}
}
