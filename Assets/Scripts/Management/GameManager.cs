using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraController cameraController;

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
    }

	private void Start()
	{
        SetCameraInitialPosition();
    }

	// Update is called once per frame
	void Update()
    {
        GetUserInput();
    }

    void GetUserInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Singleton.Instance.Gameboard.Research(DirectionType.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Singleton.Instance.Gameboard.Research(DirectionType.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Singleton.Instance.Gameboard.Research(DirectionType.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Singleton.Instance.Gameboard.Research(DirectionType.Right);
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
    }

    private void SetCameraInitialPosition()
    {
        cameraController.SetPosition(Singleton.Instance.Gameboard.selector.transform.position.x, Singleton.Instance.Gameboard.selector.transform.position.y, -100);
	}

    private void MoveUp()
    {
        cameraController.MoveCamera(DirectionType.Up);
        Singleton.Instance.Gameboard.Up();
    }
    private void MoveRight()
    {
        cameraController.MoveCamera(DirectionType.Right);
        Singleton.Instance.Gameboard.Right();
    }
    private void MoveDown()
    {
        cameraController.MoveCamera(DirectionType.Down);
        Singleton.Instance.Gameboard.Down();
    }
    private void MoveLeft()
    {
        cameraController.MoveCamera(DirectionType.Left);
        Singleton.Instance.Gameboard.Left();
    }
}
