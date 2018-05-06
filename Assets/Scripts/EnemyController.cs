using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyController : MonoBehaviour
{
	public enum EnemyType
	{
		Basic,
		Tough
	}

	[SerializeField] private EnemyType	type;                         //Tells which kind of enemy this is (basic, tougher)
	[SerializeField] private float		reloadTime;
	[SerializeField] private GameObject bullet;
	[SerializeField] private float		bulletSpeed;
	[SerializeField] private float		HP;
	[SerializeField] private float		hitDamageFromPlayer;          //How much enemy loses HP when it hits with player's bullet

	[SerializeField] private GameObject player;
	[SerializeField] private GameObject HealthPowerUpPrefab;
	[SerializeField] private GameObject ReinforcementsPowerUpPrefab;
	[SerializeField] private GameObject FirePowerPowerUpPrefab;

	private bool hasPowerUp;
	private PowerUp.PowerUpType powerUpItem;

	private OutOfBounds ofb;
	private bool inPlayArea = false;
	private float lifeTimeSeconds = 0;

	private Spline spline;
	private bool hasSpline = false;

	[SerializeField]
	private float timeToLive;
	private float timeLeft;
	private bool selfDestructionActivated;
	private bool readyToShoot = true;
	private float reloadTimeRemaining;

	void Start ()
	{
		reloadTimeRemaining = reloadTime;
		timeLeft = timeToLive;
		ofb = gameObject.AddComponent<OutOfBounds>();

		if(hasSpline)
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	}

	void Update ()
	{
		float dt = Time.deltaTime;

		transform.Find("PowerUpCircle").transform.Rotate(new Vector3(0, 0, -200 * dt), Space.Self);
		lifeTimeSeconds += dt;
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
			timeLeft -= dt;
			if (timeLeft <= 0)
			{
				if (hasPowerUp)
					DropPowerUp();
				Destroy(gameObject);
			}
		}
	}

	private void Shoot()
	{
		if (!readyToShoot)
		{
			reloadTimeRemaining -= Time.deltaTime;
			if (reloadTimeRemaining <= 0)
			{
				readyToShoot = true;
				reloadTimeRemaining = reloadTime;
			}
		}

		// Shoot if enemy has reloaded and is in camera area
		if (readyToShoot && inPlayArea)
		{
			GameObject newBullet = Instantiate(bullet);
			SetupBullet(newBullet);
			readyToShoot = false;
		}
	}

	// Setup the position and velocity of the new bullet
	private void SetupBullet(GameObject newBullet)
	{
		if (type == EnemyType.Basic || type == EnemyType.Tough)
		{
			newBullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			newBullet.SetActive(true);

			Vector2 toPlayer = player.transform.position - transform.position;
			float x = toPlayer.x;
			float y = toPlayer.y;
			
			if(x == 0 || y == 0)
			{
				newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
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

			newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(x_speed, y_speed);
		}
	}

	// Collision handler
	void OnTriggerEnter2D(Collider2D other)
	{
		// Enemy hits player's bullet
		if (other.gameObject.tag.Equals("PlayerBullet"))
		{
			other.gameObject.GetComponent<Animator>().SetBool("explosion", true);
			other.enabled = false;
			other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			other.gameObject.GetComponent<BulletController>().ActivateSelfDestruction();

			HP -= hitDamageFromPlayer;
			if (HP <= 0)
			{
				GetComponent<Animator>().SetBool("dead", true);
				GetComponent<Collider2D>().enabled = false;
				selfDestructionActivated = true;
				player.GetComponent<PlayerController>().IncreaseSuperPower();
			}
		}
	}

	public void SetupSpline(List<Vector2> list)
	{
		hasSpline = true;
		spline = gameObject.AddComponent<Spline>();
		spline.Setup(list);
	}

	public void SetPowerUp(PowerUp.PowerUpType powerUp)
	{
		hasPowerUp = true;
		powerUpItem = powerUp;
	}

	private void DropPowerUp()
	{
		if (powerUpItem == PowerUp.PowerUpType.Health)
		{
			GameObject newPowerUp = Instantiate(HealthPowerUpPrefab);
			newPowerUp.SetActive(true);
			newPowerUp.transform.position = transform.position;
		}
		else if (powerUpItem == PowerUp.PowerUpType.Reinforcements)
		{
			GameObject newPowerUp = Instantiate(ReinforcementsPowerUpPrefab);
			newPowerUp.SetActive(true);
			newPowerUp.transform.position = transform.position;
		}
		else if (powerUpItem == PowerUp.PowerUpType.FirePower)
		{
			GameObject newPowerUp = Instantiate(FirePowerPowerUpPrefab);
			newPowerUp.SetActive(true);
			newPowerUp.transform.position = transform.position;
		}
	}
}
