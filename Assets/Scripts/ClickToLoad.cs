using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoad : MonoBehaviour {

	public Slider LoadingBar;
	public GameObject LoadingImage;
	public GameObject InfoImage;

	private AsyncOperation async;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // difficulty: 1 == easy, 2 == normal, 3 == hard
    public void StartFirstLevelOnDifficulty(int difficulty)
    {
        gameManager.SetDifficulty(difficulty);
        ClickAsync(1);
    }

	public void ClickAsync(int level)
	{
		//InfoImage.SetActive(false);
		//LoadingImage.SetActive(true);
		StartCoroutine(LoadLevelWithBar(level));
	}

	public void LoadMainMenu()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene(0);
	}


	IEnumerator LoadLevelWithBar(int level)
	{
		async = SceneManager.LoadSceneAsync(level);
		while (!async.isDone)
		{
			LoadingBar.value = async.progress;
			yield return null;
		}
	}
}
