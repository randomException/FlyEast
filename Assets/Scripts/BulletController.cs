using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	private OutOfBounds outOfBoundsInstance;

    public float SelfDestructionTime;
    private bool selfDestruction = false;
	private float timeLeft;

	void Start()
	{
		outOfBoundsInstance = gameObject.AddComponent<OutOfBounds>();
		timeLeft = SelfDestructionTime;
	}

	void Update () {

		if (!outOfBoundsInstance.IsInPlayArea(transform.position.x, transform.position.y))
			Destroy(gameObject);

		if (selfDestruction)
		{
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0)
				Destroy(gameObject);
		}
	}

	public void ActivateSelfDestruction()
	{
		selfDestruction = true;
	}
}
