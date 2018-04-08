using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;

    private float difficultyDamageTakenMultiplier = 1.0f;
    public float DifficultyDamageTakenMultiplier {
        get { return difficultyDamageTakenMultiplier; }
        private set { difficultyDamageTakenMultiplier = value; }
    }

    private const float easyMultiplier = 0.75f;
    private const float normalMultiplier = 1.0f;
    private const float hardMultiplier = 1.25f;

    // Game Instance Singleton
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetDifficulty(int difficulty)
    {
        if(difficulty == 0)
        {
            DifficultyDamageTakenMultiplier = easyMultiplier;
        }
        if (difficulty == 1)
        {
            DifficultyDamageTakenMultiplier = normalMultiplier;
        }
        if (difficulty == 2)
        {
            DifficultyDamageTakenMultiplier = hardMultiplier;
        }
    }
}
