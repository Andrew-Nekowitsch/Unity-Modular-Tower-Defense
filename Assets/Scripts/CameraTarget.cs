using UnityEngine;

public class CameraTarget: MonoBehaviour
{
	public Vector3 TargetPosition;
	public Vector3 Offset = Vector3.zero;

	public void SetPos(Vector3 pos)
	{
		TargetPosition = pos;
	}

	public void SetOffset(Vector3 o)
	{
		Offset = o;
	}
}
