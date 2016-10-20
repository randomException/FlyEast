using UnityEngine;
using System.Collections;

public class ExitOnClick : MonoBehaviour {

	public void ExitTheGame()
	{
		//won't work in editor, only in built game
		Application.Quit();
	}
}
