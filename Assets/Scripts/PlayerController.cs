using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;						//Movement speed
	public float bulletSpeed;				//Players bullet movement speed
	public GameObject bullet;				//Instance of player's bullet. Will be used to create new bullets 
	public GameObject friendlyPlane;        //Instance of friendly planes. Will be used to create new friendlies
	public Sprite friendlySprite;			//Sprite of friendly plane which stays with player
	public float reloadTime;				//Tells the time between bullets are fired
	public float HP;                        //Player's health points
	private float maxHP;					//Indicates max HP that player can have
	public Image HealthBar;                 //UI element which shows the amount of HP
	public Image SuperPowerMeter;           //UI element which shows the amount of super power
	public Image SuperPowerImage;           //UI image of super power
	public GameObject Background;			//Backgroung gameobject

	private float reloadTimeRemaining;		//How much time is left before next shooting
	private bool readyToShoot;              //Tells if player is ready to shoot new bullets
	private int bulletPerShooting;			//Tells how many bullets the layer shoots at the same time

	private int hitDamage;                  //How many HPs player is going to lose when hitted by enemy planes or enemy bullets
	private float superPower;               //Tells how much of the super power has been filled (10 == full, 0 == empty)
	private bool superPowerReady;           //Tells if super power is ready to use
	private int maxSuperPower;              //Tells how many super power points player has to have so the super power activates

	private int reviveHealth;               //Tells how much player gains HP when obtained health pack

	private bool topFriend;                 //Tells if payer has at the moment a friendly plane on top
	private bool belowFriend;               //Tells if payer has at the moment a friendly plane below

	private Animator animator;              //Player animator
	private float rainRate;                 //How often rain is visible and invisible
	private float rainTimeLeft;             //When rain is going to switch type next time

	public Image dangerIndicator;           //Red UI Image for danger indicator (== low health)
	private bool danger;                    //Tells if danger in on now
	private float dangerBlinkRate;			//How often danger indicator blinks
	private float dangerTimeLeft;			//When the indicator is going to flash next time
	private bool dangerDarkRed;				//Tells whether next blink is dark red
	private float dangerBlinkLimit;         //Tells the HP limit after which blink starts

	private bool isDead;                    //Tells if player is dead

	public GameObject winImage;             //Win indicator
	public GameObject loseImage;            //Lose indicator

	// Use this for initialization
	void Start () {
		hitDamage = -5;
		reviveHealth = 25;
		maxHP = HP;

		bulletPerShooting = 2;
		reloadTimeRemaining = reloadTime;
		readyToShoot = true;

		superPower = 0;
		superPowerReady = false;
		maxSuperPower = 20;
		SuperPowerMeter.fillAmount = 0;

		topFriend = false;
		belowFriend = false;

		animator = GetComponent<Animator>();
		rainRate = 0.2f;
		rainTimeLeft = rainRate;

		danger = false;
		dangerBlinkRate = 0.3f;
		dangerTimeLeft = dangerBlinkRate;
		dangerDarkRed = false;
		dangerBlinkLimit = 0.8f;

		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Move the background
		if (Background.transform.position.x > -355)
			Background.transform.position = new Vector3(Background.transform.position.x - Time.deltaTime * 5, Background.transform.position.y, Background.transform.position.z);
		//WIN THE GAME if backgroung has moved to location '.355'
		else
			WinTheGame();

		//Raining
		if(rainTimeLeft > 0)
		{
			rainTimeLeft -= Time.deltaTime;
		}
		else
		{
			Background.transform.Find("rain").GetComponent<SpriteRenderer>().enabled = !Background.transform.Find("rain").GetComponent<SpriteRenderer>().enabled;
			rainTimeLeft = rainRate;
		}

		//Danger indicator blinking
		if (danger)
		{
			if(dangerTimeLeft > 0)
			{
				dangerTimeLeft -= Time.deltaTime;
			}
			else
			{
				if (dangerDarkRed)
					dangerIndicator.color = new Color(dangerIndicator.color.r, dangerIndicator.color.g, dangerIndicator.color.b, 1);
				else
					dangerIndicator.color = new Color(dangerIndicator.color.r, dangerIndicator.color.g, dangerIndicator.color.b, dangerBlinkLimit);

				dangerTimeLeft = dangerBlinkRate;
				dangerDarkRed = !dangerDarkRed;
			}
		}

		//reset velocity to zero
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

		//Movement: up, down, right and left. Shoot when moving
		//Play area: x: -23 to +23 and y: -10 to +10
		//if player is out of play area, moving further is not allowed
		//Move player with keys W, A, S and D
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
			if (transform.position.y <= 10)
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, speed);
			Shoot();
		}
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
			if (transform.position.y >= -10)
				GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -speed);
			Shoot();
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
			if (transform.position.x <= /*23*/ 18)
				GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
			Shoot();
		}
		else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			if (transform.position.x >= /*-23*/ -18)
				GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
			Shoot();
		}

		//Use super power with Space
		if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(1)) && superPowerReady)
		{
			UseSuperPower();
		}


		//Move and shot with mouse
		Vector3 target = transform.position;
		if (Input.GetMouseButton(0) && !isDead)
		{
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target.z = transform.position.z;

			if(target.y >= 10)
				target.y = 10;
			if (target.y <= -10)
				target.y = -10;

			Shoot();
		}
		transform.position = Vector3.MoveTowards(transform.position, target, speed * 10 * Time.deltaTime);

		if (isDead)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -speed);
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

		//Has the turrets been reloaded now? If yes -> shoot
		if (readyToShoot && HP > 0)
		{
			for (int i = 1; i <= bulletPerShooting; i++)
			{
				GetComponent<AudioSource>().Play();
				GameObject newBullet = Instantiate(bullet);
				SetupBullet(newBullet, i / (bulletPerShooting * 1.0f + 1.0f));
			}

			readyToShoot = false;
		}

	}

	//Setup the position and velocity of the new bullet
	void SetupBullet(GameObject aBullet, float location_multiplier /*string turret*/)
	{
		float height = GetComponent<Renderer>().bounds.size.y;

		aBullet.transform.position =
			new Vector3(transform.position.x,
			(transform.position.y + height / 2 - height * location_multiplier),
			transform.position.z);

		aBullet.SetActive(true);
		aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);

	}
	
	//Ends the game with notifications
	void gameOver()
	{
		animator.SetBool("playerDies", true);
		isDead = true;

		loseImage.SetActive(true);

		StartCoroutine(WaitForRestart());

	}

	//Activated when player wins
	void WinTheGame()
	{
		winImage.SetActive(true);

		StartCoroutine(WaitForRestart());
	}

	//When died or won the game, wait 3s before restarting the level
	IEnumerator WaitForRestart()
	{
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("MainMenu");
	}

	//Wait for t seconds and then destory 'gameObject'
	IEnumerator DestroyObjectAfterWait(float t, GameObject gameObject)
	{
		yield return new WaitForSeconds(t);

		Destroy(gameObject);
	}

	//Collision handler
	void OnTriggerEnter2D(Collider2D other)
	{
		//Player hits enemy bullets
		if (other.gameObject.tag == "Bullet")
		{
			other.gameObject.GetComponent<Animator>().SetBool("explosion", true);
			other.enabled = false;

			StartCoroutine(DestroyObjectAfterWait(0.2f, other.gameObject));
			ChangeHealth(hitDamage);
		}
		else if (other.gameObject.tag == "Enemy")
		{
			ChangeHealth(hitDamage);
		}
		else if (other.gameObject.tag == "HealthPU")
		{
			transform.Find("PowerupSound").GetComponent<AudioSource>().Play();
			ChangeHealth(reviveHealth);
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "BackupPU")
		{
			transform.Find("PowerupSound").GetComponent<AudioSource>().Play();

			if (!topFriend)
			{
				CreateNewFriend(new Vector2(transform.position.x - 1, transform.position.y - 2.5f), true, "top");
				topFriend = true;
			}
			if (!belowFriend)
			{
				CreateNewFriend(new Vector2(transform.position.x - 1, transform.position.y + 2.5f), true, "below");
				belowFriend = true;
			}
			
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "BulletPU")
		{
			if(bulletPerShooting < 4)
				bulletPerShooting += 1;

			Destroy(other.gameObject);
		}

	}

	//The player will now know that he has not a friendly plane on following position
	public void setFriendlyAsFalse(string pos)
	{
		if (pos.Equals("top"))
			topFriend = false;
		else
			belowFriend = false;
	}

	//Increase or decrease player's health points
	void ChangeHealth(int amount)
	{
		HP += amount;
		if (HP <= 0)
		{
			GetComponent<AudioSource>().Play();
			gameOver();
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

	//Increase super power. If full -> super power is ready
	public void IncreaseSuperPower()
	{
		superPower += 1;
		if (superPower >= maxSuperPower)
		{
			superPowerReady = true;
			superPower = maxSuperPower;
			SuperPowerMeter.enabled = false;

			//SuperPowerImage.GetComponent<Animator>().SetBool("MeterIsFull", true);
			GetComponent<SuperPowerImageController>().SetFull(true);
		}
		SuperPowerMeter.fillAmount = superPower / maxSuperPower;

	}

	//Activate the super power
	void UseSuperPower()
	{
		superPower = 0;
		superPowerReady = false;
		SuperPowerMeter.fillAmount = 0;
		SuperPowerMeter.enabled = true;

		GetComponent<SuperPowerImageController>().SetFull(false);

		CreateNewFriend(new Vector2(-50, -10), false);
		CreateNewFriend(new Vector2(-40, -5), false);
		CreateNewFriend(new Vector2(-30, 0), false);
		CreateNewFriend(new Vector2(-40, 5), false);
		CreateNewFriend(new Vector2(-50, 10), false);
	}

	//create new friendly planes
	void CreateNewFriend(Vector2 pos, bool child, string posToPlayer = "")
	{
		GameObject newFriendly = Instantiate(friendlyPlane);
		newFriendly.transform.position = pos;
		newFriendly.SetActive(true);

		if (child)
		{
			newFriendly.transform.parent = transform;
			newFriendly.GetComponent<FriendlyPlaneController>().setType("stay");
			newFriendly.GetComponent<FriendlyPlaneController>().setPosition(posToPlayer);
			newFriendly.GetComponent<SpriteRenderer>().sprite = friendlySprite;
		}
	}

	public bool IsDead()
	{
		return isDead;
	}
}