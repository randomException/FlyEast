using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

	public GameObject player;			//Player object

	private float offset_y;				//The distance between camera and player when game starts

	// Use this for initialization
	void Start()
	{
		offset_y = transform.position.y - player.transform.position.y;
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(0, player.transform.position.y * 0.3f + offset_y, -10);
	}
}
