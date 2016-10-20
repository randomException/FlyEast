using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour {

	public GameObject info;
	private bool visible;

	void start()
	{
		visible = false;
	}

	public void Show()
	{
		visible = !visible;
		info.SetActive(visible);
	}
}
