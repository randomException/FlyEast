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
		aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -bulletSpeed);

	}

	//Collision handler
	void OnCollisionEnter2D(Collision2D other)
	{
		//Enemy hits enemy bullet
		if (other.gameObject.tag == "Bullet")
		{
			Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
		}

		//Enemy hits player's bullet
		if (other.gameObject.tag == "PlayerBullet")
		{
			HP -= 10;
			if (HP <= 0)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
