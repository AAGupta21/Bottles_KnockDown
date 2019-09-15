using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject BallManager = null;
    [SerializeField] private GameObject ObjectManager = null;
    [SerializeField] private GameObject Text = null;
    [SerializeField] private GameObject MainMenuObject_1 = null;
    [SerializeField] private GameObject MainMenuObject_2 = null;
    [SerializeField] private GameObject RestartButton = null;
    [SerializeField] private GameObject MainMenuButton = null;

    private bool IsRestartButtonActive = false;

    private void Update()
    {
        if (GameManager.gameStage == GameStage.LoadMenu)
            LoadMenu();

        if(!IsRestartButtonActive && GameManager.gameStage == GameStage.CleanBottles)
        {
            IsRestartButtonActive = true;
            DisplayRestartButton();
        }
    }

    public void LoadMenu()
    {
        GameManager.gameStage = GameStage.MainMenu;

        BallManager.SetActive(false);
        ObjectManager.SetActive(false);
        Text.SetActive(false);
        RestartButton.SetActive(false);
        MainMenuButton.SetActive(false);

        MainMenuObject_1.SetActive(true);
        MainMenuObject_2.SetActive(true);

        IsRestartButtonActive = false;
    }

    private void DisplayRestartButton()
    {
        RestartButton.SetActive(true);
        MainMenuButton.SetActive(true);
    }

    public void StartGame()
    {
        BallManager.SetActive(true);
        ObjectManager.SetActive(true);
        Text.SetActive(true);

        MainMenuObject_1.SetActive(false);
        MainMenuObject_2.SetActive(false);

        GameManager.gameStage = GameStage.LoadGame;

        IsRestartButtonActive = false;
    }

    public void RestartGame()
    {
        RestartButton.SetActive(false);
        MainMenuButton.SetActive(false);

        GameManager.gameStage = GameStage.LoadGame;

        IsRestartButtonActive = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}