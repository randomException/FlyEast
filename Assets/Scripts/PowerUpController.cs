using UnityEngine;
using System.Collections;

public class PowerUpController : MonoBehaviour {

	private float timeToLive;           //How long the powerup will remain before it disapears
	private float timeAsInvisible;      //When powerup blinks, how long it is disappeared
	private float blinkRate;            //How often powerup blinks
	private float timeToblink;          //Tells when the object blinks again
	private float timeToVisible;        //Tells when the object is visible again
	private bool visible;				//Tells if object is visible at the moment

	// Use this for initialization
	void Start () {
		timeToLive = 10;
		timeAsInvisible = 0.1f;
		blinkRate = 2.5f;

		timeToblink = blinkRate;
		timeToVisible = 0;

	}
	
	// Update is called once per frame
	void Update () {
		DecreaseLifeTime(Time.deltaTime);
		
	}

	void DecreaseLifeTime(float time)
	{
		timeToLive -= time;
		
		if(timeToLive <= 0)
		{
			Destroy(gameObject);
		}

		if (visible)
		{
			timeToblink -= time;
			if(timeToblink <= 0)
			{
				GetComponent<SpriteRenderer>().enabled = false;
				visible = false;
				timeToVisible = timeAsInvisible;
			}
		}
		else
		{
			timeToVisible -= time;
			if(timeToVisible <= 0)
			{
				GetComponent<SpriteRenderer>().enabled = true;
				visible = true;
				blinkRate /= 2;
				timeToblink = blinkRate;
			}
		}

	}
}
