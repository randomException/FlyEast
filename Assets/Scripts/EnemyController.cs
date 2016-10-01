using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float reloadTime;
	public GameObject bullet;
	public float bulletSpeed;
	public float HP;
	public string movementMode;
	public float movementAngle;
	public float movementSpeed;

	private bool readyToShoot;
	private float reloadTimeRemaining;
	//time that the plane has been living
	private int lifeTime=0;

	// Use this for initialization
	void Start () {
		readyToShoot = true;
		reloadTimeRemaining = reloadTime;
	}
	
	// Update is called once per frame
	void Update () {
		//pendant: replace by deltatime.
		lifeTime++;
		Shoot();
		transform.eulerAngles = new Vector3 (0,0,getRotationAngle());
		//here we need to put a function that will make the object position to increment
		//in x and y according to the transform angle.
		//transform.Translate(Mathf.Cos(transform.rotation.z/180*Mathf.PI)*Time.deltaTime,Mathf.Sin(transform.rotation.z/180*Mathf.PI)*Time.deltaTime,0);

		//transform.position += myVector;
		//transform.RotateAround (transform.position, Vector3.forward, );
		//transform.rotation = new Quaternion(getCurrentAngle(), 0, 0, 1);
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

		//Has the turrets been reloaded now? If yes -> shoot
		if (readyToShoot)
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
			HP -= 10;
			if (HP <= 0)
			{
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
}
