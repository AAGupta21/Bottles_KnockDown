using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMesh ScoreText = null;
    [SerializeField] private TextMesh LifeText = null;
    [SerializeField] private int Life = 3;
    [SerializeField] private int ScorePerBounce = 25;
    [SerializeField] private int ScorePerShot = 100;

    public static GameStage gameStage = GameStage.Shooting;

    public int scoreVal = 0;

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        BallManager.Lives = Life;
        scoreVal = 0;
        ScoreText.text = (scoreVal).ToString();
        gameStage = GameStage.ResetBallLoc;
    }

    private void Update()
    {
        if (gameStage == GameStage.GotOne)
        {
            scoreVal += BallScript.BounceCount * ScorePerBounce + ScorePerShot;
            ScoreText.text = (scoreVal).ToString();
            BallScript.BounceCount = 0;
            gameStage = GameStage.SetGame;
        }

        if (gameStage == GameStage.GameOver)
        {
            ScoreText.text = "Final Score : " + scoreVal;
        }

        if (gameStage != GameStage.GameOver || gameStage != GameStage.MainMenu)
        {
            LifeText.text = BallManager.Lives.ToString();
        }

        if(gameStage == GameStage.LoadGame)
        {
            StartGame();
        }
    }
}