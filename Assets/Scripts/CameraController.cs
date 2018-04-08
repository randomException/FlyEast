using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public PlayerController Player;
	private float offset_y;

	void Start()
	{
		offset_y = transform.position.y - Player.transform.position.y;
	}

	void Update()
	{
		if(!Player.IsDead())
			transform.position = new Vector3(0, Player.transform.position.y * 0.3f + offset_y, -10);
	}
}
