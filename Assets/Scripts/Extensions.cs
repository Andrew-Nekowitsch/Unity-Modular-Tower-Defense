using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
	public static void DestroyChildrenRecursive(this Transform transform)
	{
		foreach (Transform t in transform)
		{
			t.DestroyChildrenRecursive();
		}
		Object.Destroy(transform.gameObject);
	}
}
