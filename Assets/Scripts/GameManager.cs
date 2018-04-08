using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;
    private float difficultyTakenDamageMultiplier;

    private const float easyMultiplier = 0.8f;
    private const float normalMultiplier = 1f;
    private const float hardMultiplier = 1.25f;

    // Game Instance Singleton
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public float DifficultyTakenDamageMultiplier {
        get { return difficultyTakenDamageMultiplier; }
        set { difficultyTakenDamageMultiplier = value; }
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
        if (difficulty == 1)
        {
            difficultyTakenDamageMultiplier = easyMultiplier;
        }
        if (difficulty == 2)
        {
            difficultyTakenDamageMultiplier = normalMultiplier;
        }
        if (difficulty == 3)
        {
            difficultyTakenDamageMultiplier = hardMultiplier;
        }
    }
}
