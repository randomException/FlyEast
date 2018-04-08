using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyController : MonoBehaviour {
	public string type;							//Tells which kind of enemy this is (basic, tougher)

	public float ReloadTime;                    //Tells the time between bullets are fired
	public GameObject Bullet;                   //Instance of enemies' bullets. Will be used to create new bullets 
	public float BulletSpeed;                   //Bullet movement speed
	public float HP;                            //Enemy's health points

	private bool hasPowerUp;                    //indicates if enemy has powerup to drop after dead
	private string powerUpItem;					//indicates which powerup enemy drops when dead

	private bool readyToShoot;                  //Tells if enemy is ready to shoot a new bullet
	private float reloadTimeRemaining;          //How much time is left before next shooting
	public float HitDamage;						//How much enemy loses HP when it hits with player's bullet
	
	private float lifeTimeSeconds;              //Time that the enemy has been living in seconds

	private OutOfBounds outOfBoundsInstance;	//'Out of bounds' class instance
	private bool inGameArea;                    //Tells if enemy has entered game area (== camera area)

	private Spline splineInstance;              //'Out of bounds' class instance for curves
	private bool hasSpline = false;             //Tells if object has a spline object

	public GameObject player;                   //Player game object
	public GameObject HealthPU;                 //Instance of health power up gameobject
	public GameObject BackupPU;                 //Instance of backup power up gameobject
	public GameObject BulletPU;                 //Instance of bullet power up gameobject

	private bool selfDestruction;				// Tells if enemy is going to disappear after certain time limit
	public float timeToLive;					// How long the bullet lies after self destruction set on
	private float timeLeft;                     // How much time is left before destory

    public bool InGameArea
    {
        get
        {
            return inGameArea;
        }

        set
        {
            inGameArea = value;
        }
    }

    // Use this for initialization
    void Start () {
		readyToShoot = true;
		reloadTimeRemaining = ReloadTime;
		lifeTimeSeconds = 0;

		outOfBoundsInstance = gameObject.AddComponent<OutOfBounds>();
        inGameArea = false;

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

		if (!inGameArea)
		{
			if (outOfBoundsInstance.IsInCameraArea(transform.position.x, transform.position.y))
                inGameArea = true;
		}
		if (inGameArea)
		{
			if (!outOfBoundsInstance.IsInPlayArea(transform.position.x, transform.position.y))
				Destroy(gameObject);
		}

		if (hasSpline)
		{
			Vector2 newLocation = splineInstance.LocationInSplineAtPercentagePoint(lifeTimeSeconds / 5);
			transform.position = new Vector3(newLocation.x, newLocation.y, 0);
		}

		if (selfDestruction)
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
				reloadTimeRemaining = ReloadTime;
			}
		}

		//Has the turrets been reloaded now and the plane is in camera area? If yes -> shoot
		if (readyToShoot && inGameArea)
		{
			GameObject newBullet = Instantiate(Bullet);
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
				aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletSpeed, 0);
				return;
			}

			//ORIGINAL LINES
			//float angle = Random.Range(-180, 180);
			//float x_speed = Mathf.Cos(angle) * bulletSpeed;
			//float y_speed = Mathf.Sin(angle) * bulletSpeed;

			float x_speed = (x / y) / (x / y + y / x) * BulletSpeed;
			if (x < 0)
				x_speed = -x_speed;

			float y_speed = (y / x) / (x / y + y / x) * BulletSpeed;
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

			HP -= HitDamage;
			if (HP <= 0)
			{
				GetComponent<Animator>().SetBool("dead", true);
				GetComponent<Collider2D>().enabled = false;

				selfDestruction = true;

				player.GetComponent<PlayerController>().IncreaseSuperPower();
			}
		}
	}


	//Setup enemy's spline curv
	public void setupSpline(List<Vector2> list)
	{
		hasSpline = true;
		splineInstance = gameObject.AddComponent<Spline>();
		splineInstance.SetupSplinePoints(list);
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
