using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

	//Out of bounds borders
	//x: [-25, 28] and y: [-15, 15]
	//Object is in play area if x_min <= player <= x_max
	//                      and y_min <= player <= y_max

	float x_max = 28;
	float x_min = -25;
	float y_max = 15;
	float y_min = -15;


	//Camera area
	//x: [-23, 23] and y: [-10, 10]
	float camera_x_max = 23;
	float camera_x_min = -23;
	float camera_y_max = 15;
	float camera_y_min = -15;

	//return true if object is inside the OutOfBounds lines, otherwise false
	public bool IsInPlayArea(float x, float y)
	{
		if (x <= x_max && x >= x_min
			&& y <= y_max && y >= y_min)
			return true;
		else
			return false;
	}

	//return true if object is inside the play area, otherwise false
	public bool IsInCameraArea(float x, float y)
	{
		if (x <= camera_x_max && x >= camera_x_min
			&& y <= camera_y_max && y >= camera_y_min)
			return true;
		else
			return false;
	}
}
