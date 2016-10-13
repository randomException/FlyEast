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
	public float reloadTime;				//Tells the time between bullets are fired
	public float HP;                        //Player's health points
	private float maxHP;					//Indicates max HP that player can have
	public Text gameOverText;               //UI text which appears when player dies
	public Image HealthBar;                 //UI element which shows the amount of HP
	public GameObject Background;			//Backgroung gameobject

	private float reloadTimeRemaining;		//How much time is left before next shooting
	private bool readyToShoot;              //Tells if player is ready to shoot new bullets
	private int bulletPerShooting;			//Tells how many bullets the layer shoots at the same time

	private int hitDamage;                  //How many HPs player is going to lose when hitted by enemy planes or enemy bullets
	private int superPower;                 //Tells how much of the super power has been filled (10 == full, 0 == empty)
	private bool superPowerReady;           //Tells if super power is ready to use
	private int maxSuperPower;              //Tells how many super power points player has to have so the super power activates

	private int reviveHealth;				//Telss how much player gains HP when obtained health pack

	// Use this for initialization
	void Start () {
		gameOverText.text = "";
		hitDamage = -5;
		reviveHealth = 25;
		maxHP = HP;

		bulletPerShooting = 2;
		reloadTimeRemaining = reloadTime;
		readyToShoot = true;

		superPower = 0;
		superPowerReady = false;
		maxSuperPower = 5;
	}
	
	// Update is called once per frame
	void Update () {
		//Move the background
		Background.transform.position = new Vector3(Background.transform.position.x - Time.deltaTime * 5, Background.transform.position.y, Background.transform.position.z);

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
			if (transform.position.x <= 23)
				GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
			Shoot();
		}
		else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			if (transform.position.x >= -23)
				GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
			Shoot();
		}

		//Use super power with Space
		if (Input.GetKey(KeyCode.Space) && superPowerReady)
		{
			UseSuperPower();
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
		if (readyToShoot)
		{
			for (int i = 1; i <= bulletPerShooting; i++)
			{
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
		gameOverText.text = "Game Over!";

		StartCoroutine(WaitForRestart());

	}

	//When died wait 3s before restarting the level
	IEnumerator WaitForRestart()
	{
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("Main");
	}

	//Collision handler
	void OnTriggerEnter2D(Collider2D other)
	{
		//Player hits enemy bullets
		if (other.gameObject.tag == "Bullet")
		{
			ChangeHealth(hitDamage);
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Enemy")
		{
			ChangeHealth(hitDamage);
		}
		else if (other.gameObject.tag == "PowerUp")
		{
			ChangeHealth(reviveHealth);
			Destroy(other.gameObject);
		}
	}

	//Increase or decrease player's health points
	void ChangeHealth(int amount)
	{
		HP += amount;
		if (HP <= 0)
		{
			gameOver();
		}
		else if (HP > maxHP)
			HP = maxHP;

		HealthBar.fillAmount = HP / maxHP;
	}

	//Increase super power. If full -> super power is ready
	public void IncreaseSuperPower()
	{
		superPower += 1;
		if (superPower >= maxSuperPower)
		{
			superPowerReady = true;
		}
	}

	//Activate the super power
	void UseSuperPower()
	{
		superPower = 0;
		superPowerReady = false;

		CreateNewFriend(new Vector2(-50, -10));
		CreateNewFriend(new Vector2(-40, -5));
		CreateNewFriend(new Vector2(-30, 0));
		CreateNewFriend(new Vector2(-40, 5));
		CreateNewFriend(new Vector2(-50, 10));
	}

	//create new friendly planes
	void CreateNewFriend(Vector2 pos)
	{
		GameObject newFriendly = Instantiate(friendlyPlane);
		newFriendly.transform.position = pos;
		newFriendly.SetActive(true);
	}
}