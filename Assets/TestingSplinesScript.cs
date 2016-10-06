using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestingSplinesScript : MonoBehaviour {

	public Vector2 v1;
	public Vector2 v2;
	public Vector2 v3;
	public Vector2 v4;

	public GameObject testBall;

	private List<Vector2> pointList;

	// Use this for initialization
	/*void Start () {
		pointList = new List<Vector2>();
		pointList.Add(v1);
		pointList.Add(v2);
		pointList.Add(v3);
		pointList.Add(v4);

		//Draw();
	}*/

	// Update is called once per frame
	void Update() {
		pointList = new List<Vector2>();
		pointList.Add(v1);
		pointList.Add(v2);
		pointList.Add(v3);
		pointList.Add(v4);

		for (float i = 0; i <= 100; i++)
		{
			GameObject newBall = Instantiate(testBall);
			Vector2 newPosition = NewLocation(i / 100);
			newBall.transform.position = new Vector3(newPosition.x, newPosition.y, 0);
			newBall.SetActive(true);
		}
	}

	public Vector2 NewLocation(float t)
	{
		return Mathf.Pow(1 - t, 3) * pointList[0] + 3 * t * Mathf.Pow(1 - t, 2) * pointList[1] +
			3 * t * t * Mathf.Pow(1 - t, 3) * pointList[2] + Mathf.Pow(t, 3) * pointList[3];
	}
}
