using UnityEngine;
using System.Collections;

public class FriendlyPlaneController : MonoBehaviour {

	public float speed;                     //Movement speed
	public float reloadTime;                //Tells the time between bullets are fired

	public GameObject bullet;               //Instance of enemies' bullets. Will be used to create new bullets 
	public float bulletSpeed;               //Bullet movement speed

	private float reloadTimeRemaining;      //How much time is left before next shooting
	private bool readyToShoot;              //Tells if friend is ready to shoot new bullets


	private OutOfBounds ofb;                //'Out of bounds' class instance
	private bool inPlay;                    //Tells if friend has entered game area (== camera area)

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

		ofb = gameObject.AddComponent<OutOfBounds>();
		inPlay = false;
	}
	
	// Update is called once per frame
	void Update () {
		Shoot();

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
			//LeftTurret
			GameObject newBullet1 = Instantiate(bullet);
			SetupBullet(newBullet1, "LeftTurret");

			//RightTurret
			GameObject newBullet2 = Instantiate(bullet);
			SetupBullet(newBullet2, "RightTurret");

			readyToShoot = false;
		}

	}

	//Setup the position and velocity of the new bullet
	void SetupBullet(GameObject aBullet, string turret)
	{
		aBullet.transform.position =
			new Vector3(transform.Find(turret).transform.position.x,
			transform.Find(turret).transform.position.y,
			transform.Find(turret).transform.position.z);
		aBullet.SetActive(true);
		aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);

	}
}
