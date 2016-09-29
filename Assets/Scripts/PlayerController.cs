using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float enemy_speed;
	public float bulletSpeed;
	public GameObject bullet;
	public GameObject enemyPlane;
	public float reloadTime;
	public float HP;
	public Text gameOverText;

	private float reloadTimeRemaining;
	private bool readyToShoot;

	private int hitDamage;

	// Use this for initialization
	void Start () {
		gameOverText.text = "";
		hitDamage = -5;
		reloadTimeRemaining = reloadTime;
		readyToShoot = true;
		Timer();
	}
	
	// Update is called once per frame
	void Update () {
		//reset velocity to zero
		GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

		//Movement: up, down, right and left. Shoow when moving
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, speed);
			Shoot();
		}
		else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -speed);
			Shoot();
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
			Shoot();
		}
		else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
			Shoot();
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
		aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);

	}

	//The timer of the game. Defines when enemies appears.
	void Timer()
	{
		StartCoroutine(Wait(1));
	}

	//Defines how much we are waiting before enemies appears
	IEnumerator Wait(float time)
	{
		yield return new WaitForSeconds(time);
		createNewEnemy(new Vector2(23, 6), -Mathf.PI);
		createNewEnemy(new Vector2(23, 1), -Mathf.PI);
		createNewEnemy(new Vector2(23, -4), -Mathf.PI);

		yield return new WaitForSeconds(8);
		createNewEnemy(new Vector2(23, -4), -Mathf.PI);
		createNewEnemy(new Vector2(23, -9), -Mathf.PI);
		createNewEnemy(new Vector2(23, -13), -Mathf.PI);
	}
	
	//Ends the game with notifications
	void gameOver()
	{
		gameOverText.text = "Game Over!";

		StartCoroutine(WaitForRestart());

	}

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
			changeHealth(hitDamage);
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Enemy")
		{
			changeHealth(hitDamage);
		}
	}

	/*void OnCollisionEnter2d(Collision2D other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			Debug.logger.Log("MAYDAY!");
		}
	}*/

	//Creates a new enemy to the given postition and directions
	//Rotation is given in radians. -PI == -180 ==> moving from right to left
	void createNewEnemy(Vector2 pos, float rot)
	{
		GameObject newEnemy = Instantiate(enemyPlane);
		newEnemy.transform.position = pos;
		newEnemy.SetActive(true);

		newEnemy.GetComponent<Rigidbody2D>().velocity =
			new Vector2(Mathf.Cos(rot) * enemy_speed, Mathf.Sin(rot) * enemy_speed);
	}

	void changeHealth(int amount)
	{
		HP += amount;
		if(HP <= 0)
		{
			gameOver();
		}
	}
}