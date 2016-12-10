using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillreadyTextController : MonoBehaviour {

	public Image SkillreadyImage;			// Image of skillready text
	
	public Sprite State1;                   // 1st "animation" sprite
	public Sprite State2;                   // 2nd "animation" sprite

	private float timeBetweenSprites;       // How much time is between sprites
	private float timeLeft;                 // How much time is left before next sprite

	private int i;                          // indicator of which is current "animation" sprite
	private List<Sprite> sprite_list;       // list of "animation" sprites

	public bool activated;


	void Start () {
		sprite_list = new List<Sprite>();
		sprite_list.Add(State1);
		sprite_list.Add(State2);

		timeBetweenSprites = 0.2f;
		timeLeft = timeBetweenSprites;

		//activated = false;
	}
	
	void Update () {
		if (activated)
		{
			timeLeft -= Time.deltaTime;

			if (timeLeft <= 0)
			{
				if (i == 0)
					i = 1;
				else
					i = 0;

				SkillreadyImage.sprite = sprite_list[i];

				timeLeft = timeBetweenSprites;
			}
		}
	}

	public void Activate()
	{
		i = 0;
		activated = true;
	}

	public void UnActivate()
	{
		activated = false;
	}
}
