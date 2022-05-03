using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleSelector : MonoBehaviour
{
	public Vector2 Position;
	public CameraTarget cameraTarget;

	private void Awake()
	{
		if (cameraTarget == null)
			cameraTarget = GetComponent<CameraTarget>();
	}

	public void SetPos(Vector2 pos)
	{
		transform.position = new Vector3(pos.x, pos.y, 0);
		Position = (Vector2)transform.position;
		cameraTarget.SetPos(Position);
	}

	public void ShiftSelector(DirectionType dir)
	{
		switch (dir)
		{
			case DirectionType.Up:
				Position.y += Gameboard.MODULE_SIZE;
				break;
			case DirectionType.Down:
				Position.y -= Gameboard.MODULE_SIZE;
				break;
			case DirectionType.Left:
				Position.x -= Gameboard.MODULE_SIZE;
				break;
			case DirectionType.Right:
				Position.x += Gameboard.MODULE_SIZE;
				break;
		}
		SetPos(new Vector3(Position.x, Position.y));
	}
}
