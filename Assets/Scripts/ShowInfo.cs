using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour {

	public GameObject info;
	public bool visible;

	public void Show()
	{
		visible = !visible;
		info.SetActive(visible);
	}
}
