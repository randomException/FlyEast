using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spline : MonoBehaviour {

	private List<Vector2> pointList;	//List of points which an enemy is going to fly through

	public void SetupSplinePoints(List<Vector2> points)
	{
		pointList = points;
	}

	public Vector2 LocationInSplineAtPercentagePoint(float t)
	{
		return Mathf.Pow(1 - t, 3) * pointList[0] + 3 * t * Mathf.Pow(1 - t, 2) * pointList[1] +
			3 * t * t * Mathf.Pow(1 - t, 3) * pointList[2] + Mathf.Pow(t, 3) * pointList[3];
	}
}
