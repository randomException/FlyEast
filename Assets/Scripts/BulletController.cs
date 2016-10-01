using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private OutOfBounds ofb;			//'Out of bounds' class instance

	void Start()
	{
		ofb = gameObject.AddComponent<OutOfBounds>();
	}

	// Update is called once per frame
	void Update () {

		if (!ofb.IsInPlayArea(transform.position.x, transform.position.y))
			Destroy(gameObject);
	}
}
