using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoad : MonoBehaviour {

	[SerializeField] private Slider		loadingBar;
	[SerializeField] private Text		LoadingText;
	[SerializeField] private GameObject	loadingImage;
	[SerializeField] private GameObject	info;
	[SerializeField] private string[]	LoadingTexts;

	private GameManager gameManager;
	private AsyncOperation async;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void StartFirstLevelOnDifficulty(int difficulty)
    {
        gameManager.SetDifficulty(difficulty);
        LoadingText.text = LoadingTexts[difficulty];
        ClickAsync(1);
    }

	public void ClickAsync(int level)
	{
		info.SetActive(false);
		loadingImage.SetActive(true);
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
			loadingBar.value = async.progress;
			yield return null;
		}
	}
}
