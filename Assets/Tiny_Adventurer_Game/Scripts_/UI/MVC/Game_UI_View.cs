using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_UI_View : MonoBehaviour
{
    #region MVC - View
    private Game_UI_Controller gameUIController_;

    public void SetGameUIController(Game_UI_Controller gameUIController)
    {
        gameUIController_ = gameUIController;
    }

    public Game_UI_Controller GetGameUIController()
    {
        return gameUIController_;
    }

    #endregion

    public GameManager GM_;
    public TMPro.TextMeshProUGUI coinText_;
    public Slider healthSlider_;
    public GameObject pause_UI_;
    public GameObject gameOver_UI_;
    public GameObject gameIsFinished_UI_;

    //Buttons
    public Button mainMenuButton_1;
    public Button restartButton_1;
    public Button mainMenuButton_2;
    public Button restartButton_2;
    public Button mainMenuButton_3;
    public Button restartButton_3;

    public enum GameUIState
    {
        Gameplay,
        Pause,
        GameOver,
        GameIsFinished
    }

    public GameUIState gameUICurrState;

    private void Awake()
    {
        mainMenuButton_1.onClick.AddListener(GM_.ReturnToMainMenu);
        restartButton_1.onClick.AddListener(GM_.Restart);
        mainMenuButton_2.onClick.AddListener(GM_.ReturnToMainMenu);
        restartButton_2.onClick.AddListener(GM_.Restart);
        mainMenuButton_3.onClick.AddListener(GM_.ReturnToMainMenu);
        restartButton_3.onClick.AddListener(GM_.Restart);
    }

    private void Start()
    {
        SwitchUIState(GameUIState.Gameplay);
    }

    void Update()
    {
        healthSlider_.value = GM_.playerCharacter_.GetComponent<Health>().currentHealthPercentage;
        coinText_.text = GM_.playerCharacter_.coin_.ToString();
    }

    public void SwitchUIState(GameUIState gameUIState)
    {
        pause_UI_.SetActive(false);
        gameOver_UI_.SetActive(false);
        gameIsFinished_UI_.SetActive(false);

        Time.timeScale = 1;

        switch (gameUIState)
        {
            case GameUIState.Gameplay:
                break;

            case GameUIState.Pause:
                Time.timeScale = 0;
                pause_UI_.SetActive(true);
                break;

            case GameUIState.GameOver:
                gameOver_UI_.SetActive(true);
                break;

            case GameUIState.GameIsFinished:
                gameIsFinished_UI_.SetActive(true);
                break;

        }

        gameUICurrState = gameUIState;
    }

    public void TogglePauseUI()
    {
        if (gameUICurrState == GameUIState.Gameplay)
        {
            SwitchUIState(GameUIState.Pause);
        }
        else if (gameUICurrState == GameUIState.Pause)
        {
            SwitchUIState(GameUIState.Gameplay);
        }
    }

}
