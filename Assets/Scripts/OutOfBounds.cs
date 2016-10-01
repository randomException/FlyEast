using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

	//Out of bounds borders
	//x: [-23, 28] and y: [-15, 15]
	//Object is in play area if x_min <= player <= x_max
	//                      and y_min <= player <= y_max

	float x_max = 28;
	float x_min = -23;
	float y_max = 15;
	float y_min = -15;


	//return true if object is inside the play area, otherwise false
	public bool IsInPlayArea(float x, float y)
	{
		if (x <= x_max && x >= x_min
			&& y <= y_max && y >= y_min)
			return true;
		else
			return false;
	}
}
