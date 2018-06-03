using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    private GameManager gameManager;
	public float speed;
	public float bulletSpeed;
	public GameObject BulletPrefab; 
	public GameObject friendlyPlane;
	public Sprite friendlySprite;
	public float reloadTime;
	public float HP;
	private float maxHP;
	public Image HealthBar;
	public Image SuperPowerMeter;
	public Image SuperPowerImage;
	public GameObject Background;

	private float reloadTimeRemaining;
	private bool readyToShoot = true;
	private int bulletPerShooting = 2;

	private int takenHitDamage = -5;
	private float superPowerAmount = 0;
	private bool superPowerReady = false;
	private int maxSuperPower = 25;

	private int reviveHealth = 25;

	private bool hasTopFriend = false;
	private bool hasBelowFriend = false;

	private Animator animator;
	private float rainRate = 0.2f;
	private float rainTimeLeft;

	public Image dangerIndicator;
	private bool danger = false;
	private float dangerBlinkRate = 0.3f;
	private float dangerTimeLeft;
	private bool dangerDarkRed = false;
	private float dangerBlinkLimit = 0.8f;

	private bool isDead = false;

	public Canvas canvas;

	public GameObject winImage;
	public GameObject loseImage;

	public Image SkillreadyImage;

	private bool isInvisible = false;
	private bool blinking = false;
	private float blinkRate = 0.2f;
	private float timeLeft;
	private int blinkTimes = 3;
	private int blinkCount = 0;

	private bool playerReady = false;

	void Start ()
	{
        gameManager = GameManager.Instance;
		maxHP = HP;
		reloadTimeRemaining = reloadTime;
		SuperPowerMeter.fillAmount = 0;

		animator = GetComponent<Animator>();
		rainTimeLeft = rainRate;

		dangerTimeLeft = dangerBlinkRate;
		timeLeft = blinkRate;
	}
	
	void Update ()
	{
		if (!playerReady)
		{
			gameObject.transform.position += new Vector3(Time.deltaTime * 10, 0, 0);
			if(gameObject.transform.position.x >= -15.5f)
			{
				playerReady = true;
			}
			return;
		}

		MoveBackground();
		BlinkPlayer();
		RainAnimation();
		DangerIndicatorBlinking();
		GetInputs();

		if (isDead)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -speed);
		}
	}

	private void MoveBackground()
	{
		//Move the background
		if (Background.transform.position.x > -355)
			Background.transform.position += new Vector3(-Time.deltaTime * 5, 0, 0);
		//WIN THE GAME if backgroung has moved to location '-355'
		else
			WinTheGame();
	}

	private void BlinkPlayer()
	{
		if (blinking)
		{
			if (blinkCount < blinkTimes)
			{
				if (!isInvisible)
				{
					timeLeft -= Time.deltaTime;
					if (timeLeft <= 0)
					{
						gameObject.GetComponent<SpriteRenderer>().color = new Color(
							gameObject.GetComponent<SpriteRenderer>().color.r,
							gameObject.GetComponent<SpriteRenderer>().color.g,
							gameObject.GetComponent<SpriteRenderer>().color.b, 0.2f);

						isInvisible = true;
						timeLeft = blinkRate;
					}
				}
				else
				{
					timeLeft -= Time.deltaTime;
					if (timeLeft <= 0)
					{
						gameObject.GetComponent<SpriteRenderer>().color = new Color(
							gameObject.GetComponent<SpriteRenderer>().color.r,
							gameObject.GetComponent<SpriteRenderer>().color.g,
							gameObject.GetComponent<SpriteRenderer>().color.b, 1);

						isInvisible = false;
						timeLeft = blinkRate;

						blinkCount++;
					}
				}
			}
			else
			{
				blinking = false;
			}
		}
	}

	private void RainAnimation()
	{
		if (rainTimeLeft > 0)
		{
			rainTimeLeft -= Time.deltaTime;
		}
		else
		{
			Background.transform.Find("rain").GetComponent<SpriteRenderer>().enabled =
				!Background.transform.Find("rain").GetComponent<SpriteRenderer>().enabled;
			rainTimeLeft = rainRate;
		}
	}

	private void DangerIndicatorBlinking()
	{
		if (danger)
		{
			if (dangerTimeLeft > 0)
			{
				dangerTimeLeft -= Time.deltaTime;
			}
			else
			{
				if (dangerDarkRed)
					dangerIndicator.color = new Color(dangerIndicator.color.r, dangerIndicator.color.g, dangerIndicator.color.b, 1);
				else
					dangerIndicator.color = new Color(dangerIndicator.color.r, dangerIndicator.color.g, dangerIndicator.color.b,
						dangerBlinkLimit);

				dangerTimeLeft = dangerBlinkRate;
				dangerDarkRed = !dangerDarkRed;
			}
		}
	}

	private void GetInputs()
	{
		// Reset velocity to zero
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

		// Use super power with right mouse button
		if (Input.GetMouseButton(1) && superPowerReady)
		{
			UseSuperPower();
			transform.Find("ClickSound").GetComponent<AudioSource>().Play();
		}

		// Return to main menu with 'esc'
		if (Input.GetKey(KeyCode.Escape))
		{
			transform.Find("ClickSound").GetComponent<AudioSource>().Play();
			SceneManager.LoadScene("MainMenu");
		}

		// Pause the game with 'space'
		if (Input.GetKeyDown(KeyCode.Space))
		{
			transform.Find("ClickSound").GetComponent<AudioSource>().Play();
			canvas.GetComponent<PauseGame>().SwitchPauseState();
		}


		//Move and shot with mouse
		Vector3 target = transform.position;
		if (Input.GetMouseButton(0) && !isDead)
		{
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target.z = transform.position.z;

			if (target.y >= 10)
				target.y = 10;
			if (target.y <= -10)
				target.y = -10;

			Shoot();
		}
		transform.position = Vector3.MoveTowards(transform.position, target, speed * 10 * Time.deltaTime);
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

		if (readyToShoot && HP > 0)
		{
			for (int i = 1; i <= bulletPerShooting; i++)
			{
				GetComponent<AudioSource>().Play();
				GameObject newBullet = Instantiate(BulletPrefab);
				SetupBullet(newBullet, i / (bulletPerShooting * 1.0f + 1.0f));
			}
			readyToShoot = false;
		}
	}

	private void SetupBullet(GameObject bullet, float location_multiplier)
	{
		float height = GetComponent<Renderer>().bounds.size.y;
		bullet.transform.position =
			new Vector3(transform.position.x,
						transform.position.y + height / 2 - height * location_multiplier,
						transform.position.z);

		bullet.SetActive(true);
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
	}

	private void GameOver()
	{
		animator.SetBool("playerDies", true);
		isDead = true;

		if (winImage.activeSelf)
		{
			winImage.SetActive(false);
			loseImage.SetActive(true);
		}
		else
		{
			loseImage.SetActive(true);
			StartCoroutine(WaitForRestart());
		}
	}

	private void WinTheGame()
	{
		if (!IsDead())
		{
			winImage.SetActive(true);
			StartCoroutine(WaitForRestart());
		}
	}

	private IEnumerator WaitForRestart()
	{
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("MainMenu");
	}

	private IEnumerator DestroyObjectAfterWait(float time, GameObject gameObject)
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Player hits enemy bullets
		if (other.gameObject.tag.Equals("Bullet"))
		{
			other.gameObject.GetComponent<Animator>().SetBool("explosion", true);
			other.enabled = false;

			StartCoroutine(DestroyObjectAfterWait(0.2f, other.gameObject));
			transform.Find("HitSound").GetComponent<AudioSource>().Play();
			ChangeHealth(takenHitDamage);
		}
		else if (other.gameObject.tag.Equals("Enemy"))
		{
			ChangeHealth(takenHitDamage);
			transform.Find("HitSound").GetComponent<AudioSource>().Play();
		}
		else if (other.gameObject.tag.Equals("HealthPU"))
		{
			transform.Find("PowerupSound").GetComponent<AudioSource>().Play();
			ChangeHealth(reviveHealth);
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag.Equals("BackupPU"))
		{
			transform.Find("PowerupSound").GetComponent<AudioSource>().Play();
			if (!hasTopFriend)
			{
				CreateNewFriend(new Vector2(transform.position.x - 1, transform.position.y - 2.5f), true,
					FriendlyPlaneController.FriendlyPosition.Top);
				hasTopFriend = true;
			}
			if (!hasBelowFriend)
			{
				CreateNewFriend(new Vector2(transform.position.x - 1, transform.position.y + 2.5f), true,
					FriendlyPlaneController.FriendlyPosition.Bottom);
				hasBelowFriend = true;
			}
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag.Equals("BulletPU"))
		{
			if(bulletPerShooting < 4)
				bulletPerShooting += 1;
			Destroy(other.gameObject);
		}
	}

	public void SetFriendlyAsDied(FriendlyPlaneController.FriendlyPosition pos)
	{
		if (pos == FriendlyPlaneController.FriendlyPosition.Top)
			hasTopFriend = false;
		else
			hasBelowFriend = false;
	}

	private void ChangeHealth(float amount)
	{
		if (amount < 0)
		{
			blinking = true;
			timeLeft = blinkRate;
			blinkCount = 0;
            amount *= gameManager.DifficultyDamageTakenMultiplier;
		}
        HP += amount;
        if (HP <= 0)
		{
			GetComponent<Collider2D>().enabled = false;
			transform.Find("DeathSound").GetComponent<AudioSource>().Play();
			GameOver();
		}
		else if (HP > maxHP)
			HP = maxHP;

		float alpha = (255 - (HP / maxHP) * 255) / 255;
		if (alpha < dangerBlinkLimit)
		{
			dangerIndicator.color = new Color(dangerIndicator.color.r, dangerIndicator.color.g, dangerIndicator.color.b, alpha);
			danger = false;
		}
		else
		{
			danger = true;
		}
		HealthBar.fillAmount = HP / maxHP;
	}

	public void IncreaseSuperPower()
	{
		superPowerAmount += 1;
		if (superPowerAmount >= maxSuperPower)
		{
			superPowerReady = true;
			superPowerAmount = maxSuperPower;
			SuperPowerMeter.enabled = false;

			//SuperPowerImage.GetComponent<Animator>().SetBool("MeterIsFull", true);
			GetComponent<SuperPowerImageController>().SetFull(true);

			SkillreadyImage.gameObject.SetActive(true);
			GetComponent<SkillreadyTextController>().Activate();
		}
		SuperPowerMeter.fillAmount = superPowerAmount / maxSuperPower;
	}

	private void UseSuperPower()
	{
		superPowerAmount = 0;
		superPowerReady = false;
		SuperPowerMeter.fillAmount = 0;
		SuperPowerMeter.enabled = true;

		GetComponent<SuperPowerImageController>().SetFull(false);

		SkillreadyImage.gameObject.SetActive(false);
		GetComponent<SkillreadyTextController>().UnActivate();

		CreateNewFriend(new Vector2(-50, -10), false);
		CreateNewFriend(new Vector2(-40, -5), false);
		CreateNewFriend(new Vector2(-30, 0), false);
		CreateNewFriend(new Vector2(-40, 5), false);
		CreateNewFriend(new Vector2(-50, 10), false);
	}

	private void CreateNewFriend(Vector2 pos, bool child,
		FriendlyPlaneController.FriendlyPosition posToPlayer = FriendlyPlaneController.FriendlyPosition.Top)
	{
		GameObject newFriendly = Instantiate(friendlyPlane);
		newFriendly.transform.position = pos;
		newFriendly.SetActive(true);

		if (child)
		{
			newFriendly.transform.parent = transform;
			newFriendly.GetComponent<FriendlyPlaneController>().ChangeType(FriendlyPlaneController.FriendlyType.Permanent);
			newFriendly.GetComponent<FriendlyPlaneController>().SetPosition(posToPlayer);
			newFriendly.GetComponent<SpriteRenderer>().sprite = friendlySprite;
		}
	}

	public bool IsDead()
	{
		return isDead;
	}
}