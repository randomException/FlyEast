using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

	public float reloadTime;                    //Tells the time between bullets are fired
	public GameObject bullet;                   //Instance of enemies' bullets. Will be used to create new bullets 
	public float bulletSpeed;                   //Bullet movement speed
	public float HP;                            //Enemy's health points

	private bool powerUp;							//indeicates if enemy has powerup to drop after dead

	public string movementMode;					//The way enemy moves (straight, sin, arc, etc.)
	public float movementAngle;					//??? - Joaquin
	public float movementSpeed;					//??? - Joaquin

	private bool readyToShoot;                  //Tells if enemy is ready to shoot a new bullet
	private float reloadTimeRemaining;          //How much time is left before next shooting
	private float hitDamage;					//How much enemy loses HP when it hits with player's bullet
	
	private int lifeTime=0;                     //Time that the plane has been living
	private float lifeTimeSeconds;              //Time that the plane has been living in seconds

	private OutOfBounds ofb;					//'Out of bounds' class instance
	private bool inPlay;                        //Tells if enemy has entered game area (== camera area)

	private Spline spline;                      //'Out of bounds' class instance fot curves
	private bool hasSpline = false;             //Tells if object has a spline object

	public GameObject player;                   //Player game object
	public GameObject powerUpGameObject;        //Instance of power up gameobject

	// Use this for initialization
	void Start () {
		readyToShoot = true;
		reloadTimeRemaining = reloadTime;
		hitDamage = 10;
		lifeTimeSeconds = 0;

		ofb = gameObject.AddComponent<OutOfBounds>();
		inPlay = false;

		if(hasSpline)
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
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
			Vector2 newLocation = spline.NewLocation(lifeTimeSeconds / 10);
			transform.position = new Vector3(newLocation.x, newLocation.y, 0);
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
		aBullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		aBullet.SetActive(true);

		float angle = Random.Range(-180, 180);

		float x_speed = Mathf.Cos(angle) * bulletSpeed;
		float y_speed = Mathf.Sin(angle) * bulletSpeed;

		aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x_speed, y_speed);

	}

	//Collision handler
	void OnTriggerEnter2D(Collider2D other)
	{
		//Enemy hits player's bullet
		if (other.gameObject.tag == "PlayerBullet")
		{
			Destroy(other.gameObject);
			HP -= hitDamage;
			if (HP <= 0)
			{
				player.GetComponent<PlayerController>().IncreaseSuperPower();
				if (powerUp)
					DropPowerUp();
				Destroy(gameObject);
			}
		}
	}

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
	public void setPowerUp()
	{
		powerUp = true;
	}

	//drop a powerUp to dead location
	void DropPowerUp()
	{
		GameObject newPowerUp = Instantiate(powerUpGameObject);
		newPowerUp.SetActive(true);
		newPowerUp.transform.position = transform.position;
	}
}
