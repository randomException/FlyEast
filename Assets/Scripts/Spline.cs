using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spline : MonoBehaviour {

	private List<Vector2> pointList;	//List of points which an enemy plane is going to fly through

	public void Setup(List<Vector2> list)
	{
		pointList = list;
	}

	public Vector2 NewLocation(float t)
	{
		return Mathf.Pow(1 - t, 3) * pointList[0] + 3 * t * Mathf.Pow(1 - t, 2) * pointList[1] +
			3 * t * t * Mathf.Pow(1 - t, 3) * pointList[2] + Mathf.Pow(t, 3) * pointList[3];
	}
}
