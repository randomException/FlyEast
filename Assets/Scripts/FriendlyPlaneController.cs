using UnityEngine;
using System.Collections;

public class FriendlyPlaneController : MonoBehaviour {

	public float speed;                     //Movement speed
	public float reloadTime;                //Tells the time between bullets are fired
	public string type;                     //Tells if friend will just move past the camera or stays with player ("fly" vs "stay")
	private string pos;						//Telss if friend is below or on top of player ("top" vs "below") (when type == "stay")
	public float HP;                        //Friend's health points
	private int hitDamage;                  //How many HPs friend is going to lose when hitted by enemy planes or enemy bullets

	public GameObject bullet;               //Instance of enemies' bullets. Will be used to create new bullets 
	public float bulletSpeed;               //Bullet movement speed

	private float reloadTimeRemaining;      //How much time is left before next shooting
	private bool readyToShoot;              //Tells if friend is ready to shoot new bullets


	private OutOfBounds ofb;                //'Out of bounds' class instance
	private bool inPlay;                    //Tells if friend has entered game area (== camera area)

	public GameObject player;               //Player gameobject

	private bool isInvisible;               //Tells if friendly is "invisible"
	private bool blinking;                  //Tells if friendly got damage and is blinking now
	private float blinkRate;                //How often friendly blinks
	private float timeLeft;                 //When the friendly is going to flash next time
	private int blinkTimes;                 //How many times friendly blinks
	private int blinkCount;                 //Current blink count

	// Use this for initialization
	void Start () {
		ofb = gameObject.AddComponent<OutOfBounds>();
		inPlay = false;
		hitDamage = -20;

		isInvisible = false;
		blinking = false;
		blinkRate = 0.2f;
		timeLeft = blinkRate;
		blinkTimes = 3;
		blinkCount = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (type.Equals("stay"))
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		else
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		}

		Shoot();

		if (!inPlay)
		{
			if (ofb.IsInCameraArea(transform.position.x, transform.position.y))
				inPlay = true;
		}

		//friends who statys next to player can't be out of bounds
		if (inPlay && type.Equals("fly"))
		{
			if (!ofb.IsInPlayArea(transform.position.x, transform.position.y))
				Destroy(gameObject);
		}

		//Player blinking
		if (blinking)
		{
			// if there is more blinks to come
			if (blinkCount < blinkTimes)
			{
				// if player is visible
				if (!isInvisible)
				{
					timeLeft -= Time.deltaTime;
					if (timeLeft <= 0)
					{
						gameObject.GetComponent<SpriteRenderer>().color = new Color(
							gameObject.GetComponent<SpriteRenderer>().color.r,
							gameObject.GetComponent<SpriteRenderer>().color.g,
							gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);

						isInvisible = true;
						timeLeft = blinkRate;
					}
				}
				// if player is invisible
				else
				{
					timeLeft -= Time.deltaTime;
					if (timeLeft <= 0)
					{
						gameObject.GetComponent<SpriteRenderer>().color = new Color(
							gameObject.GetComponent<SpriteRenderer>().color.r,
							gameObject.GetComponent<SpriteRenderer>().color.g,
							gameObject.GetComponent<SpriteRenderer>().color.b, 1);

						isInvisible = false;
						timeLeft = blinkRate;

						blinkCount++;
					}
				}
			}
			// if all blinks has been made
			else
			{
				blinking = false;
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
		if (readyToShoot && inPlay)
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
			blinking = true;
			timeLeft = blinkRate;
			blinkCount = 0;

			if (HP <= 0)
			{
				player.GetComponent<PlayerController>().setFriendlyAsFalse(pos);
				Destroy(gameObject);
			}
		}
	}
}
