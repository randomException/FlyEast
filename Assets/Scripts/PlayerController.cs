using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;						//Movement speed
	public float enemy_speed;				//Enemies movement speed
	public float bulletSpeed;				//Players bullet movement speed
	public GameObject bullet;				//Instance of player's bullet. Will be used to create new bullets 
	public GameObject enemyPlane;           //Instance of enemy planes. Will be used to create new enemies 
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

	private EnemyController enemyController;    //EnemyController script

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

		Timer();
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
		//MOve player with keys W, A, S and D
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

	//Calls the timer of the game
	void Timer()
	{
		StartCoroutine(Wait());
	}

	//Defines when and where enemies appears and how they move
	IEnumerator Wait()
	{
		yield return new WaitForSeconds(2);
		//1st group
		List<Vector2> list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(new Vector2(27, 6), -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(new Vector2(27, 1), -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(new Vector2(27, -4), -Mathf.PI, "none", list);

		yield return new WaitForSeconds(4);
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(new Vector2(27, -2), -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(new Vector2(27, -7), -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(new Vector2(27, -11), -Mathf.PI, "none", list);

		//Direct line
		yield return new WaitForSeconds(5f);
		CreateNewEnemy(new Vector2(4, -14), Mathf.PI * 3 / 4, "none");
		CreateNewEnemy(new Vector2(10, -14), Mathf.PI * 3 / 4, "none");
		CreateNewEnemy(new Vector2(16, -14), Mathf.PI * 3 / 4, "none");

		//3rd group (4 planes same path)
		yield return new WaitForSeconds(3);
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(new Vector2(20, -15), -Mathf.PI, "none", list);

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));

		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(3f);
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(new Vector2(20, -15), -Mathf.PI, "health", list);

		yield return new WaitForSeconds(0.5f);
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(new Vector2(20, -15), -Mathf.PI, "none", list);

		yield return new WaitForSeconds(0.5f);
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(new Vector2(20, -15), -Mathf.PI, "none", list);

		//4th Group
		//TODO groups 4-n
		//LEVEL TIME == 3min
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

	//Creates a new enemy to the given postition and directions
	//Rotation is given in radians. -PI == -180 ==> moving from right to left
	void CreateNewEnemy(Vector2 pos, float rot, string powerup, List<Vector2> list = null)
	{
		GameObject newEnemy = Instantiate(enemyPlane);
		newEnemy.transform.position = pos;
		newEnemy.SetActive(true);

		newEnemy.GetComponent<Rigidbody2D>().velocity =
			new Vector2(Mathf.Cos(rot) * enemy_speed, Mathf.Sin(rot) * enemy_speed);

		//rot and Matf.PI are multiplied so we can get right rotation angle
		//left == 0, down == 90, right == 180 and up == 270 (in degrees)
		rot += Mathf.PI;
		newEnemy.transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * rot), Space.Self);

		if (list != null)
		{
			newEnemy.GetComponent<EnemyController>().setupSpline(list);
		}

		//If enemy holds a power up, activate the PowerUpCircle child element and drop power up when destroyed by player or friendlies
		if (!powerup.Equals("none"))
		{
			newEnemy.transform.Find("PowerUpCircle").gameObject.SetActive(true);
			newEnemy.GetComponent<EnemyController>().setPowerUp(powerup);
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