using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {
	public string type;							//Tells which kindof enemy this is (basic, tougher, big)

	public float reloadTime;                    //Tells the time between bullets are fired
	public GameObject bullet;                   //Instance of enemies' bullets. Will be used to create new bullets 
	public float bulletSpeed;                   //Bullet movement speed
	public float HP;                            //Enemy's health points

	private bool powerUp;                       //indicates if enemy has powerup to drop after dead
	private string powerUpItem;					//indicated wjich powerup enemy drops when dead

	public string movementMode;					//The way enemy moves (straight, sin, arc, etc.)
	public float movementAngle;					//??? - Joaquin
	public float movementSpeed;					//??? - Joaquin

	private bool readyToShoot;                  //Tells if enemy is ready to shoot a new bullet
	private float reloadTimeRemaining;          //How much time is left before next shooting
	public float hitDamage;						//How much enemy loses HP when it hits with player's bullet
	
	private int lifeTime=0;                     //Time that the plane has been living
	private float lifeTimeSeconds;              //Time that the plane has been living in seconds

	private OutOfBounds ofb;					//'Out of bounds' class instance
	private bool inPlay;                        //Tells if enemy has entered game area (== camera area)

	private Spline spline;                      //'Out of bounds' class instance fot curves
	private bool hasSpline = false;             //Tells if object has a spline object

	public GameObject player;                   //Player game object
	public GameObject HealthPU;                 //Instance of health power up gameobject
	public GameObject BackupPU;                 //Instance of backup power up gameobject
	public GameObject BulletPU;                 //Instance of bullet power up gameobject

	private bool selfDestruction;       // Tells if enemy is going to disappear after certain time limit
	public float timeToLive;            // How long the bullet lies after self destruction set on
	private float timeLeft;             // How much time is left before destory

	// Use this for initialization
	void Start () {
		readyToShoot = true;
		reloadTimeRemaining = reloadTime;
		lifeTimeSeconds = 0;

		ofb = gameObject.AddComponent<OutOfBounds>();
		inPlay = false;

		if(hasSpline)
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

		timeLeft = timeToLive;
	}
	
	// Update is called once per frame
	void Update () {

		transform.Find("PowerUpCircle").transform.Rotate(new Vector3(0, 0, -200 * Time.deltaTime), Space.Self);

		//pendant: replace by deltatime.
		lifeTime++;
		lifeTimeSeconds += Time.deltaTime;
		Shoot();
		//transform.eulerAngles = new Vector3 (0,0,getRotationAngle());
		
		//here we need to put a function that will make the object position to increment
		//in x and y according to the transform angle.
		//transform.Translate(Mathf.Cos(transform.rotation.z/180*Mathf.PI)*Time.deltaTime,Mathf.Sin(transform.rotation.z/180*Mathf.PI)*Time.deltaTime,0);

		//transform.position += myVector;
		//transform.RotateAround (transform.position, Vector3.forward, );
		//transform.rotation = new Quaternion(getCurrentAngle(), 0, 0, 1);

		if (!inPlay)
		{
			if (ofb.IsInCameraArea(transform.position.x, transform.position.y))
				inPlay = true;
		}
		if (inPlay)
		{
			if (!ofb.IsInPlayArea(transform.position.x, transform.position.y))
				Destroy(gameObject);
		}

		if (hasSpline)
		{
			Vector2 newLocation = spline.NewLocation(lifeTimeSeconds / 5);
			transform.position = new Vector3(newLocation.x, newLocation.y, 0);
		}

		if (selfDestruction)
		{
			timeLeft -= Time.deltaTime;

			if (timeLeft <= 0)
			{
				if (powerUp)
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
		if (readyToShoot && inPlay)
		{
			GameObject newBullet = Instantiate(bullet);
			SetupBullet(newBullet);

			readyToShoot = false;
		}

	}

	//Setup the position and velocity of the new bullet
	void SetupBullet(GameObject aBullet)
	{
		if (type.Equals("Basic"))
		{
			aBullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			aBullet.SetActive(true);

			Vector2 toPlayer = player.transform.position - transform.position;
			float x = toPlayer.x;
			float y = toPlayer.y;

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

		else if (type.Equals("Tougher"))
		{
			return;
		}

	}

	//Wait for t seconds and then destory 'gameObject'
	IEnumerator DestroyObjectAfterWait(float t, GameObject gameObject, bool enemy=false)
	{
		yield return new WaitForSeconds(t);

		if (enemy)
		{
			if (powerUp)
				DropPowerUp();
		}

		Destroy(gameObject);
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
			other.gameObject.GetComponent<BulletController>().activateSelfDestruction();

			HP -= hitDamage;
			if (HP <= 0)
			{
				GetComponent<Animator>().SetBool("dead", true);
				GetComponent<Collider2D>().enabled = false;
				//StartCoroutine(DestroyObjectAfterWait(0.1f, other.gameObject, true));

				selfDestruction = true;

				player.GetComponent<PlayerController>().IncreaseSuperPower();
			}
		}
	}

	//CAN BE REMOVED AFTER SPLINES WORKS 100% CORRECTLY
	float getRotationAngle(){
		switch (movementMode){
		case "straight":
			return movementAngle;
		case "sinusoidal":
			//base angle + sin(time / oscilation length)*oscillation intensity in... ¿deg?
			return movementAngle + Mathf.Sin (lifeTime / 100.00f)*40;
		case "arc":
			return movementAngle+lifeTime;
		default:
			return 0;
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
		powerUp = true;
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
