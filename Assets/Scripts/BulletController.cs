using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private OutOfBounds ofb;            // 'Out of bounds' class instance

	private bool selfDestruction;       // Tells if bullet is going to disappear after certain time limit
	public float timeToLive;            // How long the bullet lies after self destruction set on
	private float timeLeft;				// How much time is left before destory

	void Start()
	{
		ofb = gameObject.AddComponent<OutOfBounds>();
		timeLeft = timeToLive;
	}

	// Update is called once per frame
	void Update () {

		if (!ofb.IsInPlayArea(transform.position.x, transform.position.y))
			Destroy(gameObject);

		if (selfDestruction)
		{
			timeLeft -= Time.deltaTime;

			if (timeLeft <= 0)
				Destroy(gameObject);
		}
	}

	public void activateSelfDestruction()
	{
		selfDestruction = true;
	}
}
