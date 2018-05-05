using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyController : MonoBehaviour {

	[SerializeField] private string		type;                         //Tells which kind of enemy this is (basic, tougher)
	[SerializeField] private float		reloadTime;
	[SerializeField] private GameObject bullet;
	[SerializeField] private float		bulletSpeed;
	[SerializeField] private float		HP;
	[SerializeField] private float		hitDamage;                    //How much enemy loses HP when it hits with player's bullet

	[SerializeField] private GameObject player;
	[SerializeField] private GameObject HealthPU;
	[SerializeField] private GameObject BackupPU;
	[SerializeField] private GameObject BulletPU;

	private bool hasPowerUp;
	private string powerUpItem;

	private bool readyToShoot;
	private float reloadTimeRemaining;
	
	private float lifeTimeSeconds;

	private OutOfBounds ofb;
	private bool inPlayArea;

	private Spline spline;
	private bool hasSpline = false;

	[SerializeField]
	private float timeToLive;					// How long the bullet lies after self destruction set on
	private float timeLeft;                     // How much time is left before destory
	private bool selfDestructionActivated;      // Tells if enemy is going to disappear after certain time limit

	void Start () {
		readyToShoot = true;
		reloadTimeRemaining = reloadTime;
		lifeTimeSeconds = 0;

		ofb = gameObject.AddComponent<OutOfBounds>();
		inPlayArea = false;

		if(hasSpline)
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

		timeLeft = timeToLive;
	}
	
	// Update is called once per frame
	void Update () {

		transform.Find("PowerUpCircle").transform.Rotate(new Vector3(0, 0, -200 * Time.deltaTime), Space.Self);

		//pendant: replace by deltatime.
		lifeTimeSeconds += Time.deltaTime;
		Shoot();

		if (!inPlayArea)
		{
			if (ofb.IsInCameraArea(transform.position.x, transform.position.y))
				inPlayArea = true;
		}
		if (inPlayArea)
		{
			if (!ofb.IsInPlayArea(transform.position.x, transform.position.y))
				Destroy(gameObject);
		}

		if (hasSpline)
		{
			Vector2 newLocation = spline.NewLocation(lifeTimeSeconds / 5);
			transform.position = new Vector3(newLocation.x, newLocation.y, 0);
		}

		if (selfDestructionActivated)
		{
			timeLeft -= Time.deltaTime;

			if (timeLeft <= 0)
			{
				if (hasPowerUp)
					DropPowerUp();
				Destroy(gameObject);
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
			GameObject newBullet = Instantiate(bullet);
			SetupBullet(newBullet);

			readyToShoot = false;
		}

	}

	//Setup the position and velocity of the new bullet
	void SetupBullet(GameObject aBullet)
	{
		if (type.Equals("Basic") || type.Equals("Tougher"))
		{
			aBullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			aBullet.SetActive(true);

			Vector2 toPlayer = player.transform.position - transform.position;
			float x = toPlayer.x;
			float y = toPlayer.y;
			
			if(x == 0 || y == 0)
			{
				aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
				return;
			}

			//ORIGINAL LINES
			//float angle = Random.Range(-180, 180);
			//float x_speed = Mathf.Cos(angle) * bulletSpeed;
			//float y_speed = Mathf.Sin(angle) * bulletSpeed;

			float x_speed = (x / y) / (x / y + y / x) * bulletSpeed;
			if (x < 0)
				x_speed = -x_speed;

			float y_speed = (y / x) / (x / y + y / x) * bulletSpeed;
			if (y < 0)
				y_speed = -y_speed;

			
			aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x_speed, y_speed);
		}

		else
		{
			return;
		}

	}


	//Collision handler
	void OnTriggerEnter2D(Collider2D other)
	{
		//Enemy hits player's bullet
		if (other.gameObject.tag == "PlayerBullet")
		{
			other.gameObject.GetComponent<Animator>().SetBool("explosion", true);
			other.enabled = false;
			other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			other.gameObject.GetComponent<BulletController>().ActivateSelfDestruction();

			HP -= hitDamage;
			if (HP <= 0)
			{
				GetComponent<Animator>().SetBool("dead", true);
				GetComponent<Collider2D>().enabled = false;

				selfDestructionActivated = true;

				player.GetComponent<PlayerController>().IncreaseSuperPower();
			}
		}
	}


	//Setup enemy's spline curv
	public void setupSpline(List<Vector2> list)
	{
		hasSpline = true;
		spline = gameObject.AddComponent<Spline>();
		spline.Setup(list);
	}

	//set the powerUp variable to true
	public void setPowerUp(string powerItem)
	{
		hasPowerUp = true;
		powerUpItem = powerItem;
	}

	//drop a powerUp to dead location
	void DropPowerUp()
	{
		if (powerUpItem.Equals("health"))
		{
			GameObject newPowerUp = Instantiate(HealthPU);
			newPowerUp.SetActive(true);
			newPowerUp.transform.position = transform.position;
		}
		else if (powerUpItem.Equals("friend"))
		{
			GameObject newPowerUp = Instantiate(BackupPU);
			newPowerUp.SetActive(true);
			newPowerUp.transform.position = transform.position;
		}
		else if (powerUpItem.Equals("bullet"))
		{
			GameObject newPowerUp = Instantiate(BulletPU);
			newPowerUp.SetActive(true);
			newPowerUp.transform.position = transform.position;
		}
	}
}
