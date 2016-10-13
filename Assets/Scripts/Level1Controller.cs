using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1Controller : MonoBehaviour {

	public float enemy_speed;               //Enemies movement speed
	public GameObject enemyPlane;           //Instance of enemy planes. Will be used to create new enemies 

	private EnemyController enemyController;    //EnemyController script

	// Use this for initialization
	void Start () {
		Timer();
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
}
