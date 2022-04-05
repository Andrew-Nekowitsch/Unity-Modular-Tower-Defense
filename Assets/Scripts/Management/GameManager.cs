using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Gameboard gameBoard;
    public CameraController cameraController;

    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
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
            gameBoard.Research(DirectionType.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameBoard.Research(DirectionType.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameBoard.Research(DirectionType.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameBoard.Research(DirectionType.Right);
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

    private void MoveUp()
    {
        cameraController.MoveCamera(DirectionType.Up);
        gameBoard.Up();
    }
    private void MoveRight()
    {
        cameraController.MoveCamera(DirectionType.Right);
        gameBoard.Right();
    }
    private void MoveDown()
    {
        cameraController.MoveCamera(DirectionType.Down);
        gameBoard.Down();
    }
    private void MoveLeft()
    {
        cameraController.MoveCamera(DirectionType.Left);
        gameBoard.Left();
    }
}
