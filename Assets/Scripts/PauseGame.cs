using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

	private bool pause;

	public Image pauseButton;
	private Sprite originalPauseImage;
	public Sprite pausedPauseImage;

	public Image backButton;
	private Sprite originalBackImage;
	public Sprite pausedBackImage;

	// Use this for initialization
	void Start () {
		pause = false;

		originalPauseImage = pauseButton.sprite;
		originalBackImage = backButton.sprite;
	}
	

	public void Pause()
	{
		pause = !pause;

		if (pause)
		{
			Time.timeScale = 0.0f;
			pauseButton.sprite = pausedPauseImage;
			backButton.sprite = pausedBackImage;
		}
		else
		{
			Time.timeScale = 1.0f;
			pauseButton.sprite = originalPauseImage;
			backButton.sprite = originalBackImage;
		}
	}
}
