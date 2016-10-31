using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToLoad : MonoBehaviour {

	public Slider loadingBar;
	public GameObject loadingImage;
	public GameObject info;

	private AsyncOperation async;


	public void ClickAsync(int level)
	{
		info.SetActive(false);
		loadingImage.SetActive(true);
		StartCoroutine(LoadLevelWithBar(level));
	}

	public void LoadMainMenu()
	{
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
