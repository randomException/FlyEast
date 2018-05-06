using UnityEngine;
using System.Collections;

public class FriendlyPlaneController : MonoBehaviour {

	public enum FriendlyType
	{
		Permanent,
		Temporary
	}

	public enum FriendlyPosition
	{
		Top,
		Bottom
	}

	[SerializeField] private GameObject		player;
	[SerializeField] private float			speed;
	[SerializeField] private float			reloadTime;
	[SerializeField] private FriendlyType	type;
	[SerializeField] private float			HP;
	[SerializeField] private GameObject		bullet;
	[SerializeField] private float			bulletSpeed;
	private FriendlyPosition position;
	private int hitDamageTaken = -20;
	private int numberOfBullets = 2;

	private float reloadTimeRemaining;
	private bool readyToShoot;

	private SpriteRenderer spriteRenderer;


	private OutOfBounds ofb;
	private bool inPlayArea = false;

	private bool isInvisible = false;
	private bool isBlinking = false;
	private float blinkRate = 0.2f;
	private float timeLeftForNextBlink;
	private int blinkTimes = 3;
	private int blinkCount = 0;

	void Start ()
	{
		ofb = gameObject.AddComponent<OutOfBounds>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		timeLeftForNextBlink = blinkRate;

		speed = (type == FriendlyType.Permanent) ? 0 : speed;
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
		reloadTimeRemaining = reloadTime;
	}

	void Update () {
		Shoot();

		if (!inPlayArea)
		{
			if (ofb.IsInCameraArea(transform.position.x, transform.position.y))
				inPlayArea = true;
		}

		// Friends who statys next to the player can't be out of bounds
		if (inPlayArea && type == FriendlyType.Temporary)
		{
			if (!ofb.IsInPlayArea(transform.position.x, transform.position.y))
				Destroy(gameObject);
		}
		Blink();
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

		if (readyToShoot && inPlayArea)
		{
			for (int i = 1; i <= numberOfBullets; i++)
			{
				GameObject newBullet = Instantiate(bullet);
				SetupBullet(newBullet, i / (numberOfBullets + 1.0f));
			}
			readyToShoot = false;
		}
	}

	private void Blink()
	{
		float dt = Time.deltaTime;
		if (isBlinking)
		{
			if (blinkCount < blinkTimes)
			{
				if (!isInvisible)
				{
					timeLeftForNextBlink -= dt;
					if (timeLeftForNextBlink <= 0)
					{
						spriteRenderer.color = new Color(
							spriteRenderer.color.r,
							spriteRenderer.color.g,
							spriteRenderer.color.b, 0.2f);

						isInvisible = true;
						timeLeftForNextBlink = blinkRate;
					}
				}
				else
				{
					timeLeftForNextBlink -= dt;
					if (timeLeftForNextBlink <= 0)
					{
						spriteRenderer.color = new Color(
							spriteRenderer.color.r,
							spriteRenderer.color.g,
							spriteRenderer.color.b, 1);

						isInvisible = false;
						timeLeftForNextBlink = blinkRate;

						blinkCount++;
					}
				}
			}
			// if all blinks has been made
			else
			{
				isBlinking = false;
			}
		}
	}

	// Setup the position and velocity of the new bullet
	private void SetupBullet(GameObject newBullet, float location_multiplier)
	{
		float height = GetComponent<Renderer>().bounds.size.y;

		newBullet.transform.position =
			new Vector3(transform.position.x,
			(transform.position.y + height / 2 - height * location_multiplier),
			transform.position.z);

		newBullet.SetActive(true);
		newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);

	}

	public void ChangeType(FriendlyType friendType)
	{
		type = friendType;
		reloadTime *= 2; // Permanent friedlies has longer reload time
	}

	public void SetPosition(FriendlyPosition friendPos)
	{
		position = friendPos;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Friendly hits enemy bullets
		if (other.gameObject.tag == "Bullet")
		{
			ChangeHealth(hitDamageTaken);
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Enemy")
		{
			ChangeHealth(hitDamageTaken);
		}
	}

	private void ChangeHealth(int damage)
	{
		HP += hitDamageTaken;

		//Only friend that stays with player can die
		if (type == FriendlyType.Permanent)
		{
			isBlinking = true;
			timeLeftForNextBlink = blinkRate;
			blinkCount = 0;

			if (HP <= 0)
			{
				player.GetComponent<PlayerController>().SetFriendlyAsDied(position);
				Destroy(gameObject);
			}
		}
	}
}
