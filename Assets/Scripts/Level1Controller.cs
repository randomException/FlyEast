using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Type = EnemyController.EnemyType;

public class Level1Controller : MonoBehaviour {

	[SerializeField] private float		enemy_speed;
	[SerializeField] private GameObject	enemyPlane;
	[SerializeField] private GameObject	enemyPlaneTough; 

	private EnemyController enemyController;

	void Start ()
	{
		Timer();
	}

	void Timer()
	{
		StartCoroutine(LevelCreator());
	}

	//Defines when and where enemies appears and how they move
	IEnumerator LevelCreator()
	{
		// Wait for the player starting animation to end
		yield return new WaitForSeconds(1.2f);
		//Now the game really starts

		yield return new WaitForSeconds(2);                     // 0 min 2.0 s
																//1st group
		List<Vector2> list = new List<Vector2>
		{
			new Vector2(30, 0),
			new Vector2(7, -20),
			new Vector2(-11, -30),
			new Vector2(-30, 10)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Reinforcements, list);

		list = new List<Vector2>
		{
			new Vector2(30, -6),
			new Vector2(7, 16),
			new Vector2(-11, 20),
			new Vector2(-30, -10)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>
		{
			new Vector2(14, -16),
			new Vector2(1, 18),
			new Vector2(-11, -72),
			new Vector2(-30, -1)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(3);                     // 0 min 6.0 s
																//2nd group
		list = new List<Vector2>
		{
			new Vector2(25, 18),
			new Vector2(0, -12),
			new Vector2(-14, -14),
			new Vector2(-25, 18)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>
		{
			new Vector2(20, -17),
			new Vector2(0, -10),
			new Vector2(-14, 135),
			new Vector2(-23, -18)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>
		{
			new Vector2(30, 4),
			new Vector2(23, -13),
			new Vector2(-144, -50),
			new Vector2(-30, -2)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//Direct line
		yield return new WaitForSeconds(5f);                        // 0 min 11.0 s
		CreateNewEnemy(new Vector2(4, -14), Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(10, -14), Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(16, -14), Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);

		//3rd group (4 planes same path)
		yield return new WaitForSeconds(3);
		list = new List<Vector2>
		{
			new Vector2(20, -15),
			new Vector2(8, 58),
			new Vector2(-97, -135),
			new Vector2(-32, -1)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>
		{
			new Vector2(30, 0),
			new Vector2(7, -20),
			new Vector2(-11, -30),
			new Vector2(-30, -50)
		};

		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(3f);                        // 0 min 14.0 s
		list = new List<Vector2>
		{
			new Vector2(20, -15),
			new Vector2(8, 58),
			new Vector2(-97, -135),
			new Vector2(-32, -1)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Health, list);

		yield return new WaitForSeconds(0.5f);                      // 0 min 14.5 s
		list = new List<Vector2>
		{
			new Vector2(20, -15),
			new Vector2(8, 58),
			new Vector2(-97, -135),
			new Vector2(-32, -1)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(0.5f);                      // 0 min 15.0 s
		list = new List<Vector2>
		{
			new Vector2(20, -15),
			new Vector2(8, 58),
			new Vector2(-97, -135),
			new Vector2(-32, -1)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);
		
		//REUSE

		yield return new WaitForSeconds(2);                         // 0 min 17.0 s
																	//1st group
		list = new List<Vector2>
		{
			new Vector2(30, 0),
			new Vector2(7, -20),
			new Vector2(-11, -30),
			new Vector2(-30, 10)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>
		{
			new Vector2(30, -6),
			new Vector2(7, 16),
			new Vector2(-11, 20),
			new Vector2(-30, -10)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>
		{
			new Vector2(14, -16),
			new Vector2(1, 18),
			new Vector2(-11, -72),
			new Vector2(-30, -1)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.FirePower, list, Type.Tough);

		yield return new WaitForSeconds(3);                         // 0 min 20.0 s
																	//2nd group
		list = new List<Vector2>
		{
			new Vector2(25, 18),
			new Vector2(0, -12),
			new Vector2(-14, -14),
			new Vector2(-25, 18)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>
		{
			new Vector2(20, -17),
			new Vector2(0, -10),
			new Vector2(-14, 135),
			new Vector2(-23, -18)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>
		{
			new Vector2(30, 4),
			new Vector2(23, -13),
			new Vector2(-144, -50),
			new Vector2(-30, -2)
		};
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);


		yield return new WaitForSeconds(2);						// 0 min 22.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Reinforcements, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		yield return new WaitForSeconds(3);						// 0 min 26.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//Direct line
		yield return new WaitForSeconds(4f);                        // 0 min 30.0 s
		CreateNewEnemy(new Vector2(19, 30), -Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(25, 30), -Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(31, 30), -Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);

		//3rd group (4 planes same path)
		yield return new WaitForSeconds(3);							// 0 min 33.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));

		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(3f);                        // 0 min 36.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Health, list);

		yield return new WaitForSeconds(0.5f);                       // 0 min 36.5 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(0.5f);                      // 0 min 37.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);


		yield return new WaitForSeconds(2);						// 0 min 39.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.FirePower, list, Type.Tough);

		yield return new WaitForSeconds(3);						// 0 min 43.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(1);						// 0 min 44.0 s
		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Reinforcements, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

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
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Reinforcements, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(3.5f);						// 0 min 50.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//Direct line
		yield return new WaitForSeconds(4f);                        // 0 min 54.0 s
		CreateNewEnemy(new Vector2(4, -14), Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(10, -14), Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(16, -14), Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);

		//3rd group (4 planes same path)
		yield return new WaitForSeconds(3);                     // 0 min 57.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);


		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(3f);                        // 1 min 0.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Health, list);

		yield return new WaitForSeconds(0.5f);                        // 1 min 0.5 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(0.5f);                        // 1 min 1.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//REUSE

		yield return new WaitForSeconds(2);                        // 1 min 3.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		yield return new WaitForSeconds(3);                        // 1 min 6.0 s
																   //2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Reinforcements, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);


		yield return new WaitForSeconds(2);                        // 1 min 8.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Reinforcements, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(3);                        // 1 min 11.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);


		//3rd group (4 planes same path)
		yield return new WaitForSeconds(3);                        // 1 min 14.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));

		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		yield return new WaitForSeconds(3f);                        // 1 min 17.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Health, list);

		yield return new WaitForSeconds(0.5f);                        // 1 min 17.5 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(0.5f);                        // 1 min 18.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);


		yield return new WaitForSeconds(3);                        // 1 min 21.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Reinforcements, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		yield return new WaitForSeconds(2);                        // 1 min 23.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//3rd group (4 planes almost same path)
		yield return new WaitForSeconds(3);                        // 1 min 26.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -25));
		list.Add(new Vector2(8, 48));
		list.Add(new Vector2(-97, -145));
		list.Add(new Vector2(-32, -11));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -20));
		list.Add(new Vector2(8, 53));
		list.Add(new Vector2(-97, -140));
		list.Add(new Vector2(-32, -6));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.FirePower, list, Type.Tough);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -30));
		list.Add(new Vector2(8, 43));
		list.Add(new Vector2(-97, -150));
		list.Add(new Vector2(-32, -16));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//5th group
		yield return new WaitForSeconds(3);                        // 1 min 29.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(30, 22));
		list.Add(new Vector2(4, -78));
		list.Add(new Vector2(-64, 316));
		list.Add(new Vector2(-30, -12));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Health, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(32, -2));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -4));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 1));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, 16));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));

		//Direct lines
		yield return new WaitForSeconds(3);                        // 1 min 32.0 s
		CreateNewEnemy(new Vector2(15, 10), -Mathf.PI, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(20, 5), -Mathf.PI, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(25, 0), -Mathf.PI, PowerUp.PowerUpType.Reinforcements);
		CreateNewEnemy(new Vector2(20, -5), -Mathf.PI, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(15, -10), -Mathf.PI, PowerUp.PowerUpType.None);

		//LAST REUSE

		yield return new WaitForSeconds(2.5f);						// 1 min 36.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);


		yield return new WaitForSeconds(2);						// 1 min 38.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);


		yield return new WaitForSeconds(1);						// 1 min 39.0 s
		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(2);						// 1 min 41.0 s
		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(0.5f);						// 1 min 41.5 s
		//1st group
		new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Reinforcements, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(3);						// 1 min 45.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//Direct line
		yield return new WaitForSeconds(2.5f);                        // 1 min 48.0 s
		CreateNewEnemy(new Vector2(4, -14), Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(10, -14), Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(16, -14), Mathf.PI * 3 / 4, PowerUp.PowerUpType.None);

		//3rd group (4 planes same path)
		yield return new WaitForSeconds(2);                     // 1 min 51.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(2);                     // 1 min 54 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Health, list, Type.Tough);

		yield return new WaitForSeconds(0.5f);                     // 1 min 54.5 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(0.5f);                     // 1 min 55.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//REUSE

		yield return new WaitForSeconds(2);                        // 1 min 57.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(2);                     // 2 min 0.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Reinforcements, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);


		yield return new WaitForSeconds(2);                        // 2 min 2.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(3);                        // 2 min 5.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.FirePower, list);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);


		//3rd group (4 planes same path)
		yield return new WaitForSeconds(2);                       // 2 min 8.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));

		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);


		yield return new WaitForSeconds(2.5f);                        // 2 min 11.0 s
		//2nd group
		list = new List<Vector2>();
		list.Add(new Vector2(25, 18));
		list.Add(new Vector2(0, -12));
		list.Add(new Vector2(-14, -14));
		list.Add(new Vector2(-25, 18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -18));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(30, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//4th group
		list = new List<Vector2>();
		list.Add(new Vector2(29, 18));
		list.Add(new Vector2(0, -76));
		list.Add(new Vector2(-21, 337));
		list.Add(new Vector2(-22, -17));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Reinforcements, list);

		list = new List<Vector2>();
		list.Add(new Vector2(20, -17));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, 11));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 4));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, -2));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		yield return new WaitForSeconds(2);                        // 2 min 13.0 s
		//1st group
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, 10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(30, -6));
		list.Add(new Vector2(7, 16));
		list.Add(new Vector2(-11, 20));
		list.Add(new Vector2(-30, -10));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		list = new List<Vector2>();
		list.Add(new Vector2(14, -16));
		list.Add(new Vector2(1, 18));
		list.Add(new Vector2(-11, -72));
		list.Add(new Vector2(-30, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//Direct lines
		yield return new WaitForSeconds(2);                        // 2 min 15.0 s
		CreateNewEnemy(new Vector2(40, 10), -Mathf.PI, PowerUp.PowerUpType.None);
		CreateNewEnemy(new Vector2(35, 5), -Mathf.PI, PowerUp.PowerUpType.None, null, Type.Tough);
		CreateNewEnemy(new Vector2(30, 0), -Mathf.PI, PowerUp.PowerUpType.Reinforcements);
		CreateNewEnemy(new Vector2(35, -5), -Mathf.PI, PowerUp.PowerUpType.None, null, Type.Tough);
		CreateNewEnemy(new Vector2(40, -10), -Mathf.PI, PowerUp.PowerUpType.None);

		//3rd group (4 planes almost same path)
		yield return new WaitForSeconds(2);                        // 2 min 18.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(20, -15));
		list.Add(new Vector2(8, 58));
		list.Add(new Vector2(-97, -135));
		list.Add(new Vector2(-32, -1));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -10));
		list.Add(new Vector2(8, 63));
		list.Add(new Vector2(-97, -130));
		list.Add(new Vector2(-32, 4));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -5));
		list.Add(new Vector2(8, 68));
		list.Add(new Vector2(-97, -125));
		list.Add(new Vector2(-32, 9));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);
		
		list = new List<Vector2>();
		list.Add(new Vector2(20, -20));
		list.Add(new Vector2(8, 53));
		list.Add(new Vector2(-97, -140));
		list.Add(new Vector2(-32, -6));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		//5th group
		yield return new WaitForSeconds(2);                        // 2 min 21.0 s
		list = new List<Vector2>();
		list.Add(new Vector2(30, 22));
		list.Add(new Vector2(4, -78));
		list.Add(new Vector2(-64, 316));
		list.Add(new Vector2(-30, -12));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.Health, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(32, -2));
		list.Add(new Vector2(0, -10));
		list.Add(new Vector2(-14, 135));
		list.Add(new Vector2(-23, -4));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		list = new List<Vector2>();
		list.Add(new Vector2(31, 1));
		list.Add(new Vector2(23, -13));
		list.Add(new Vector2(-144, -50));
		list.Add(new Vector2(-30, 16));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list);

		//Spline for new enemy (no testSpline saved)
		list = new List<Vector2>();
		list.Add(new Vector2(30, 0));
		list.Add(new Vector2(7, -20));
		list.Add(new Vector2(-11, -30));
		list.Add(new Vector2(-30, -50));
		CreateNewEnemy(list[0], -Mathf.PI, PowerUp.PowerUpType.None, list, Type.Tough);

		//Direct lines
		yield return new WaitForSeconds(0.5f);                        // 2 min 22.0 s
		CreateNewEnemy(new Vector2(40, 10), -Mathf.PI, PowerUp.PowerUpType.None, null, Type.Tough);
		CreateNewEnemy(new Vector2(35, 5), -Mathf.PI, PowerUp.PowerUpType.None, null, Type.Tough);
		CreateNewEnemy(new Vector2(30, 0), -Mathf.PI, PowerUp.PowerUpType.None, null, Type.Tough);
		CreateNewEnemy(new Vector2(35, -5), -Mathf.PI, PowerUp.PowerUpType.None, null, Type.Tough);
		CreateNewEnemy(new Vector2(40, -10), -Mathf.PI, PowerUp.PowerUpType.None, null, Type.Tough);

	}

	// Creates a new enemy to the given postition and directions
	// Rotation is given in radians. -PI == -180 ==> moving from right to left
	void CreateNewEnemy(Vector2 pos, float rot, PowerUp.PowerUpType powerup, List<Vector2> list = null, Type type = Type.Basic)
	{
		GameObject newEnemy;
		if (type == Type.Basic)
			newEnemy = Instantiate(enemyPlane);
		else
			newEnemy = Instantiate(enemyPlaneTough);

		newEnemy.transform.position = pos;
		newEnemy.SetActive(true);

		newEnemy.GetComponent<Rigidbody2D>().velocity =
			new Vector2(Mathf.Cos(rot) * enemy_speed, Mathf.Sin(rot) * enemy_speed);

		// rot and Matf.PI are multiplied so we can get right rotation angle
		// left == 0, down == 90, right == 180 and up == 270 (in degrees)
		rot += Mathf.PI;
		newEnemy.transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * rot), Space.Self);

		if (list != null)
		{
			newEnemy.GetComponent<EnemyController>().SetupSpline(list);
		}

		// If enemy holds a power up, activate the PowerUpCircle child element and drop power up when destroyed by player or friendlies
		if (!powerup.Equals(PowerUp.PowerUpType.None))
		{
			newEnemy.transform.Find("PowerUpCircle").gameObject.SetActive(true);
			newEnemy.GetComponent<EnemyController>().SetPowerUp(powerup);
		}
	}
}
