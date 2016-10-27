using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	private bool pause;

	// Use this for initialization
	void Start () {
		pause = false;
	}
	

	public void Pause()
	{
		pause = !pause;

		if(pause)
			Time.timeScale = 0.0f;
		else
			Time.timeScale = 1.0f;
	}
}
