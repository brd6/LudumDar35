using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [HideInInspector]
    public int playerLife = 3;

    [HideInInspector]
    public int score = 0;

    [HideInInspector]
    public int hightScore = 0;

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        playerLife = 3;
        score = 0;
    }

    void MakeInstance()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    internal void InscreasePlayerScore()
    {
        score++;
        GamePlayController.instance.UpdateUI();
    }

    internal void DecreasePlayerLife()
    {

        playerLife--;

        if (playerLife <= 0)
        {
            // Game over
            if (hightScore < score)
                hightScore = score;
            GamePlayController.instance.UpdateUI();
            GamePlayController.instance.isGameOver = true;
            playerLife = 3;
            GamePlayController.instance.GameOver();
        }
        else
        {
            GamePlayController.instance.UpdateUI();
        }
    }
}
