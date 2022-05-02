using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Camera Cam;
	public CameraTarget CamTarget;

	public void SetPosition()
	{
		if (Cam == null) return;
		if (CamTarget == null) return;
		Debug.Log(CamTarget.TargetPosition);
		Debug.Log(CamTarget.Offset);
		Debug.Log(CamTarget.TargetPosition + CamTarget.Offset);
		Cam.transform.position = CamTarget.TargetPosition + CamTarget.Offset;
		Cam.transform.LookAt(CamTarget.TargetPosition);
	}
}
