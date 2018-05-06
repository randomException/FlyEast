using UnityEngine;
using System.Collections;

public class FriendlyPlaneController : MonoBehaviour {

	public enum FriendlyType
	{
		Permanent,
		Temporary
	}

	public enum FriendlyPosition
	{
		Top,
		Bottom
	}

	[SerializeField] private GameObject	player;
	[SerializeField] private float		speed;
	[SerializeField] private float		reloadTime;
	[SerializeField] private string		type;   //Tells if friend will just move past the camera or stays with player ("fly" vs "stay")
	[SerializeField] private float		HP;
	[SerializeField] private GameObject	bullet;
	[SerializeField] private float		bulletSpeed;
	private string pos;                         //Tells if friend is below or on top of player ("top" vs "below") (when type == "stay")
	private int hitDamage;						//How many HPs friend is going to lose when hitted by enemy planes or enemy bullets

	private float reloadTimeRemaining;
	private bool readyToShoot;


	private OutOfBounds ofb;
	private bool inPlayArea;

	private bool isInvisible;
	private bool isBlinking;
	private float blinkRate;
	private float timeLeftForNextBlink;
	private int blinkTimes;
	private int blinkCount;

	void Start () {
		ofb = gameObject.AddComponent<OutOfBounds>();
		inPlayArea = false;
		hitDamage = -20;

		isInvisible = false;
		isBlinking = false;
		blinkRate = 0.2f;
		timeLeftForNextBlink = blinkRate;
		blinkTimes = 3;
		blinkCount = 0;
	}

	void Update () {

		if (type.Equals("stay"))
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		else
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		}

		Shoot();

		if (!inPlayArea)
		{
			if (ofb.IsInCameraArea(transform.position.x, transform.position.y))
				inPlayArea = true;
		}

		//friends who statys next to player can't be out of bounds
		if (inPlayArea && type.Equals("fly"))
		{
			if (!ofb.IsInPlayArea(transform.position.x, transform.position.y))
				Destroy(gameObject);
		}

		//Player blinking
		if (isBlinking)
		{
			// if there is more blinks to come
			if (blinkCount < blinkTimes)
			{
				// if player is visible
				if (!isInvisible)
				{
					timeLeftForNextBlink -= Time.deltaTime;
					if (timeLeftForNextBlink <= 0)
					{
						gameObject.GetComponent<SpriteRenderer>().color = new Color(
							gameObject.GetComponent<SpriteRenderer>().color.r,
							gameObject.GetComponent<SpriteRenderer>().color.g,
							gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);

						isInvisible = true;
						timeLeftForNextBlink = blinkRate;
					}
				}
				// if player is invisible
				else
				{
					timeLeftForNextBlink -= Time.deltaTime;
					if (timeLeftForNextBlink <= 0)
					{
						gameObject.GetComponent<SpriteRenderer>().color = new Color(
							gameObject.GetComponent<SpriteRenderer>().color.r,
							gameObject.GetComponent<SpriteRenderer>().color.g,
							gameObject.GetComponent<SpriteRenderer>().color.b, 1);

						isInvisible = false;
						timeLeftForNextBlink = blinkRate;

						blinkCount++;
					}
				}
			}
			// if all blinks has been made
			else
			{
				isBlinking = false;
			}
		}
	}

	//Function for shooting
	void Shoot()
	{
		//Able to shoot == not reloading?
		if (!readyToShoot)
		{
			reloadTimeRemaining -= Time.deltaTime;
			if (reloadTimeRemaining <= 0)
			{
				readyToShoot = true;
				reloadTimeRemaining = reloadTime;
			}
		}

		//Has the turrets been reloaded now and the plane is in camera area? If yes -> shoot
		if (readyToShoot && inPlayArea)
		{
			for (int i = 1; i <= 2; i++)
			{
				GameObject newBullet = Instantiate(bullet);
				SetupBullet(newBullet, i / (3.0f));
			}

			readyToShoot = false;
		}

	}

	//Setup the position and velocity of the new bullet
	void SetupBullet(GameObject aBullet, float location_multiplier)
	{
		float height = GetComponent<Renderer>().bounds.size.y;

		aBullet.transform.position =
			new Vector3(transform.position.x,
			(transform.position.y + height / 2 - height * location_multiplier),
			transform.position.z);

		aBullet.SetActive(true);
		aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);

	}

	public void setType(string friendType)
	{
		type = friendType;
		reloadTime *= 2;
	}

	public void setPosition(string friendPos)
	{
		pos = friendPos;
	}

	//Collision handler
	void OnTriggerEnter2D(Collider2D other)
	{
		//Player hits enemy bullets
		if (other.gameObject.tag == "Bullet")
		{
			ChangeHealth(hitDamage);
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Enemy")
		{
			ChangeHealth(hitDamage);
		}
	}

	void ChangeHealth(int damage)
	{
		HP += hitDamage;

		//Only friend that stays with player can die
		if (type.Equals("stay"))
		{
			isBlinking = true;
			timeLeftForNextBlink = blinkRate;
			blinkCount = 0;

			if (HP <= 0)
			{
				player.GetComponent<PlayerController>().setFriendlyAsFalse(pos);
				Destroy(gameObject);
			}
		}
	}
}
