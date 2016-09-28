using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float bulletSpeed;
	public GameObject bullet;
	public GameObject enemyPlane;
	public float reloadTime;
	public float HP;
	public Text gameOverText;

	private float reloadTimeRemaining;
	private bool readyToShoot;
	

	// Use this for initialization
	void Start () {
		gameOverText.text = "";
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
		aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);

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
		createNewEnemy(new Vector2(-9, 6), 270);
		createNewEnemy(new Vector2(-15, 6), 270);
		createNewEnemy(new Vector2(-3, 6), 270);

		yield return new WaitForSeconds(8);
		createNewEnemy(new Vector2(9, 6), 270);
		createNewEnemy(new Vector2(15, 6), 270);
		createNewEnemy(new Vector2(3, 6), 270);
	}
	
	//Ends the game with notifications
	void gameOver()
	{
		gameOverText.text = "Game Over!";
	}

	//Collision handler
	void OnCollisionEnter2D(Collision2D other)
	{
		//Player hits his/her own bullets
		if (other.gameObject.tag == "PlayerBullet")
		{
			Debug.logger.Log("hit");
			Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
		}

		//Player hits enemy bullets
		if (other.gameObject.tag == "Bullet")
		{
			Debug.logger.Log("HIT");
			HP -= 5;
			if(HP <= 0)
			{
				gameOver();
			}
			Destroy(other.gameObject);
		}
	}

	void createNewEnemy(Vector2 pos, float rot)
	{
		GameObject newEnemy = Instantiate(enemyPlane);
		newEnemy.transform.position = pos;
		newEnemy.SetActive(true);
		newEnemy.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos((rot/360) * 2 * Mathf.PI), Mathf.Sin((rot/360) * 2 * Mathf.PI));
	}
}