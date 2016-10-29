using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SuperPowerImageController : MonoBehaviour {

	public Image SuperPowerImage;

	public Sprite OriginalSprite;			// original sprite of super power image
	public Sprite State1;					// 1st "animation" sprite
	public Sprite State2;                   // 2nd "animation" sprite
	public Sprite State3;                   // 3rd "animation" sprite
	public Sprite State4;                   // 4th "animation" sprite

	public Sprite evState1;                 // 1st Evacuation "animation" sprite
	public Sprite evState2;                 // 2nd Evacuation "animation" sprite
	public Sprite evState3;                 // 3rd Evacuation "animation" sprite
	public Sprite evState4;                 // 4th Evacuation "animation" sprite

	private float timeBetweenSprites;		// How much time is between sprites
	private float timeLeft;                 // How much time is left before next sprite

	private int i;							// indicator of which is current "animation" sprite
	private List<Sprite> sprite_list;		// list of "animation" sprites

	private bool full;						// indicates if "animation" is on or off

	// Use this for initialization
	void Start () {
		sprite_list = new List<Sprite>();
		sprite_list.Add(State1);
		sprite_list.Add(State2);
		sprite_list.Add(State3);
		sprite_list.Add(State4);

		timeBetweenSprites = 0.1f;
		timeLeft = timeBetweenSprites;

		full = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (!full)
		{
			return;
		}

		timeLeft -= Time.deltaTime;

		if(timeLeft <= 0)
		{
			i++;
			if (i > 3)
				i = 0;
			SuperPowerImage.sprite = sprite_list[i];

			timeLeft = timeBetweenSprites;
		}
	}

	// Set variable full be true => start "animation"
	public void SetFull(bool state)
	{
		full = state;

		if (full)
		{
			i = 0;
			SuperPowerImage.sprite = sprite_list[i];
		}
		else
		{
			StartCoroutine(EvacuationAnimation());
			//SuperPowerImage.sprite = OriginalSprite;
		}
	}

	IEnumerator EvacuationAnimation()
	{
		yield return new WaitForSeconds(0.1f);
		SuperPowerImage.sprite = evState1;
		yield return new WaitForSeconds(0.1f);
		SuperPowerImage.sprite = evState2;
		yield return new WaitForSeconds(0.1f);
		SuperPowerImage.sprite = evState3;
		yield return new WaitForSeconds(0.1f);
		SuperPowerImage.sprite = evState4;
		yield return new WaitForSeconds(0.1f);
		SuperPowerImage.sprite = OriginalSprite;
	}

}
