using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private PlayerController player;
	private float offset_y;

	void Start()
	{
		offset_y = transform.position.y - player.transform.position.y;
	}

	void Update()
	{
		if(!player.IsDead())
			transform.position = new Vector3(0, player.transform.position.y * 0.3f + offset_y, -10);
	}
}
