using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private OutOfBounds ofb;

	[SerializeField]
	private float timeToLive;

	private bool selfDestructionActivated;
	private float timeLeft;

	void Start()
	{
		ofb = gameObject.AddComponent<OutOfBounds>();
		timeLeft = timeToLive;
	}
	
	void Update () {

		if (!ofb.IsInPlayArea(transform.position.x, transform.position.y))
			Destroy(gameObject);

		if (selfDestructionActivated)
		{
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0)
				Destroy(gameObject);
		}
	}

	public void ActivateSelfDestruction()
	{
		selfDestructionActivated = true;
	}
}
