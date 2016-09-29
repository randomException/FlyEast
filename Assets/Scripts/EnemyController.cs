using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float reloadTime;
	public GameObject bullet;
	public GameObject player;
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
			//TODO: enemies shoot one bullet towards the player

			/*//LeftTurret
			GameObject newBullet1 = Instantiate(bullet);
			SetupBullet(newBullet1, "LeftTurret");

			//RightTurret
			GameObject newBullet2 = Instantiate(bullet);
			SetupBullet(newBullet2, "RightTurret");*/

			Vector2 player_position = new Vector2(player.transform.position.x, player.transform.position.y);
			Vector2 own_position = new Vector2(transform.position.x, transform.position.y);

			Vector2 x_axel = new Vector2(1, 0);
			Vector2 direction_to_player = new Vector2(player_position.x - own_position.x, player_position.y - own_position.y);

			float angle = Mathf.Acos(Vector2.Dot(x_axel, direction_to_player) / (x_axel.magnitude * direction_to_player.magnitude));

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
		aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);

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
