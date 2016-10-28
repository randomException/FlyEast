using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1Controller : MonoBehaviour {

	public float enemy_speed;					 //Enemies movement speed
	public GameObject enemyPlane;                //Instance of enemy planes. Will be used to create new enemies 
	public GameObject enemyPlaneTough;           //Instance of tough enemy planes. Will be used to create new enemies 

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
		yield return new WaitForSeconds(2);						// 0 min 2.0 s
		//1st group
		List<Vector2> list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "friend", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(3);						// 0 min 6.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//Direct line
		yield return new WaitForSeconds(5f);                        // 0 min 11.0 s
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
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));

		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(3f);                        // 0 min 14.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "health", list);

		yield return new WaitForSeconds(0.5f);                      // 0 min 14.5 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(0.5f);                      // 0 min 15.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);
		
		//REUSE

		yield return new WaitForSeconds(2);							// 0 min 17.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "bullet", list, "tough");

		yield return new WaitForSeconds(3);							// 0 min 20.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);


		yield return new WaitForSeconds(2);						// 0 min 22.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "friend", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		yield return new WaitForSeconds(3);						// 0 min 26.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//Direct line
		yield return new WaitForSeconds(4f);                        // 0 min 30.0 s
		CreateNewEnemy(new Vector2(19, 30), -Mathf.PI * 3 / 4, "none");
		CreateNewEnemy(new Vector2(25, 30), -Mathf.PI * 3 / 4, "none");
		CreateNewEnemy(new Vector2(31, 30), -Mathf.PI * 3 / 4, "none");

		//3rd group (4 planes same path)
		yield return new WaitForSeconds(3);							// 0 min 33.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));

		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(3f);                        // 0 min 36.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "health", list);

		yield return new WaitForSeconds(0.5f);                       // 0 min 36.5 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(0.5f);                      // 0 min 37.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);


		yield return new WaitForSeconds(2);						// 0 min 39.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "bullet", list, "tough");

		yield return new WaitForSeconds(3);						// 0 min 43.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(1);						// 0 min 44.0 s
		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, "friend", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		//NEW REUSE OF ALL ABOVE

		yield return new WaitForSeconds(2);						// 0 min 46.0 s
		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));

		yield return new WaitForSeconds(0.5f);						// 0 min 46.5 s
		//1st group
		new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "friend", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(3.5f);						// 0 min 50.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//Direct line
		yield return new WaitForSeconds(4f);                        // 0 min 54.0 s
		CreateNewEnemy(new Vector2(4, -14), Mathf.PI * 3 / 4, "none");
		CreateNewEnemy(new Vector2(10, -14), Mathf.PI * 3 / 4, "none");
		CreateNewEnemy(new Vector2(16, -14), Mathf.PI * 3 / 4, "none");

		//3rd group (4 planes same path)
		yield return new WaitForSeconds(3);                     // 0 min 57.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);


		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(3f);                        // 1 min 0.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "health", list);

		yield return new WaitForSeconds(0.5f);                        // 1 min 0.5 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(0.5f);                        // 1 min 1.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//REUSE

		yield return new WaitForSeconds(2);                        // 1 min 3.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		yield return new WaitForSeconds(3);                        // 1 min 6.0 s
																   //2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, "friend", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);


		yield return new WaitForSeconds(2);                        // 1 min 8.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "friend", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(3);                        // 1 min 11.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);


		//3rd group (4 planes same path)
		yield return new WaitForSeconds(3);                        // 1 min 14.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));

		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		yield return new WaitForSeconds(3f);                        // 1 min 17.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "health", list);

		yield return new WaitForSeconds(0.5f);                        // 1 min 17.5 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(0.5f);                        // 1 min 18.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");


		yield return new WaitForSeconds(3);                        // 1 min 21.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, "friend", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		yield return new WaitForSeconds(2);                        // 1 min 23.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//3rd group (4 planes almost same path)
		yield return new WaitForSeconds(3);                        // 1 min 26.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -25));
		list.Add(new Vector2(8, 48));
		list.Add(new Vector2(-97, -145));
		list.Add(new Vector2(-32, -11));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -20));
		list.Add(new Vector2(8, 53));
		list.Add(new Vector2(-97, -140));
		list.Add(new Vector2(-32, -6));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "bullet", list, "tough");
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -30));
		list.Add(new Vector2(8, 43));
		list.Add(new Vector2(-97, -150));
		list.Add(new Vector2(-32, -16));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//5th group
		yield return new WaitForSeconds(3);                        // 1 min 29.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(30, 22));
		list.Add(new Vector2(4, -78));
		list.Add(new Vector2(-64, 316));
		list.Add(new Vector2(-30, -12));
		CreateNewEnemy(list[0], -Mathf.PI, "health", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(32, -2));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -4));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(31, 1));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, 16));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));

		//Direct lines
		yield return new WaitForSeconds(3);                        // 1 min 32.0 s
		CreateNewEnemy(new Vector2(15, 10), -Mathf.PI, "none");
		CreateNewEnemy(new Vector2(20, 5), -Mathf.PI, "none");
		CreateNewEnemy(new Vector2(25, 0), -Mathf.PI, "friend");
		CreateNewEnemy(new Vector2(20, -5), -Mathf.PI, "none");
		CreateNewEnemy(new Vector2(15, -10), -Mathf.PI, "none");

		//LAST REUSE

		yield return new WaitForSeconds(2.5f);						// 1 min 36.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");


		yield return new WaitForSeconds(2);						// 1 min 38.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);


		yield return new WaitForSeconds(1);						// 1 min 39.0 s
		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(2);						// 1 min 41.0 s
		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(0.5f);						// 1 min 41.5 s
		//1st group
		new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "friend", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(3);						// 1 min 45.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//Direct line
		yield return new WaitForSeconds(2.5f);                        // 1 min 48.0 s
		CreateNewEnemy(new Vector2(4, -14), Mathf.PI * 3 / 4, "none");
		CreateNewEnemy(new Vector2(10, -14), Mathf.PI * 3 / 4, "none");
		CreateNewEnemy(new Vector2(16, -14), Mathf.PI * 3 / 4, "none");

		//3rd group (4 planes same path)
		yield return new WaitForSeconds(2);                     // 1 min 51.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(2);                     // 1 min 54 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "health", list, "tough");

		yield return new WaitForSeconds(0.5f);                     // 1 min 54.5 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(0.5f);                     // 1 min 55.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//REUSE

		yield return new WaitForSeconds(2);                        // 1 min 57.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(2);                     // 2 min 0.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, "friend", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);


		yield return new WaitForSeconds(2);                        // 2 min 2.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(3);                        // 2 min 5.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "bullet", list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");


		//3rd group (4 planes same path)
		yield return new WaitForSeconds(2);                       // 2 min 8.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));

		CreateNewEnemy(list[0], -Mathf.PI, "none", list);


		yield return new WaitForSeconds(2.5f);                        // 2 min 11.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, "friend", list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		yield return new WaitForSeconds(2);                        // 2 min 13.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//Direct lines
		yield return new WaitForSeconds(2);                        // 2 min 15.0 s
		CreateNewEnemy(new Vector2(40, 10), -Mathf.PI, "none");
		CreateNewEnemy(new Vector2(35, 5), -Mathf.PI, "none", null, "tough");
		CreateNewEnemy(new Vector2(30, 0), -Mathf.PI, "friend");
		CreateNewEnemy(new Vector2(35, -5), -Mathf.PI, "none", null, "tough");
		CreateNewEnemy(new Vector2(40, -10), -Mathf.PI, "none");

		//3rd group (4 planes almost same path)
		yield return new WaitForSeconds(2);                        // 2 min 18.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -10));
		list.Add(new Vector2(8, 63));
		list.Add(new Vector2(-97, -130));
		list.Add(new Vector2(-32, 4));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -5));
		list.Add(new Vector2(8, 68));
		list.Add(new Vector2(-97, -125));
		list.Add(new Vector2(-32, 9));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -20));
		list.Add(new Vector2(8, 53));
		list.Add(new Vector2(-97, -140));
		list.Add(new Vector2(-32, -6));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		//5th group
		yield return new WaitForSeconds(2);                        // 2 min 21.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(30, 22));
		list.Add(new Vector2(4, -78));
		list.Add(new Vector2(-64, 316));
		list.Add(new Vector2(-30, -12));
		CreateNewEnemy(list[0], -Mathf.PI, "health", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(32, -2));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -4));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		list = new List<Vector2>();
		list.Add(new Vector2(31, 1));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, 16));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list);

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));
		CreateNewEnemy(list[0], -Mathf.PI, "none", list, "tough");

		//Direct lines
		yield return new WaitForSeconds(0.5f);                        // 2 min 22.0 s
		CreateNewEnemy(new Vector2(40, 10), -Mathf.PI, "none", null, "tough");
		CreateNewEnemy(new Vector2(35, 5), -Mathf.PI, "none", null, "tough");
		CreateNewEnemy(new Vector2(30, 0), -Mathf.PI, "none", null, "tough");
		CreateNewEnemy(new Vector2(35, -5), -Mathf.PI, "none", null, "tough");
		CreateNewEnemy(new Vector2(40, -10), -Mathf.PI, "none", null, "tough");

	}

	//Creates a new enemy to the given postition and directions
	//Rotation is given in radians. -PI == -180 ==> moving from right to left
	void CreateNewEnemy(Vector2 pos, float rot, string powerup, List<Vector2> list = null, string type = "basic")
	{
		GameObject newEnemy;
		if (type.Equals("basic"))
			newEnemy = Instantiate(enemyPlane);
		else
			newEnemy = Instantiate(enemyPlaneTough);

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
