using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

	private bool isPaused = false;

	[SerializeField] private Image backButton;
	[SerializeField] private Image pauseButton;
	[SerializeField] private Sprite pausedPauseImage;
	[SerializeField] private Sprite pausedBackImage;

	private Sprite originalPauseImage;
	private Sprite originalBackImage;

	// Use this for initialization
	void Start ()
	{
		originalPauseImage = pauseButton.sprite;
		originalBackImage = backButton.sprite;
	}
	

	public void SwitchPauseState()
	{
		isPaused = !isPaused;
		if (isPaused)
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
