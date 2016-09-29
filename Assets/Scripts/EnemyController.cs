using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float reloadTime;
	public GameObject bullet;
	public float bulletSpeed;
	public float HP;

	private bool readyToShoot;
	private float reloadTimeRemaining;

	// Use this for initialization
	void Start () {
		readyToShoot = true;
		reloadTimeRemaining = reloadTime;
	}
	
	// Update is called once per frame
	void Update () {
		Shoot();
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
}
