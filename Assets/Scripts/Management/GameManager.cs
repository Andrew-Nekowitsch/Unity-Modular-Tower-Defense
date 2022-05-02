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
		cameraController.SetPosition();
	}

	private void MoveUp()
	{
		Singleton.Instance.Gameboard.Up();
		cameraController.SetPosition();
	}
	private void MoveRight()
	{
		Singleton.Instance.Gameboard.Right();
		cameraController.SetPosition();
	}
	private void MoveDown()
	{
		Singleton.Instance.Gameboard.Down();
		cameraController.SetPosition();
	}
	private void MoveLeft()
	{
		Singleton.Instance.Gameboard.Left();
		cameraController.SetPosition();
	}
}
