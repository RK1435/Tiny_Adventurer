using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics;

public class Game_UI_Manager : MonoBehaviour
{
    public GameManager GM_;
    [SerializeField] private TMPro.TextMeshProUGUI coinText_;
    [SerializeField] private Slider healthSlider_;
    [SerializeField] private GameObject pause_UI_;
    [SerializeField] private GameObject gameOver_UI_;
    [SerializeField] private GameObject gameIsFinished_UI_;

    //Buttons
    [SerializeField] private Button mainMenuButton_1;
    [SerializeField] private Button mainMenuButton_2;
    [SerializeField] private Button mainMenuButton_3;
    [SerializeField] private Button restartButton_1; 
    [SerializeField] private Button restartButton_2;
    [SerializeField] private Button restartButton_3;

    private enum GameUIState
    {
        Gameplay,
        Pause,
        GameOver,
        GameIsFinished
    }

    GameUIState gameUICurrState;

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

    // Update is called once per frame
    void Update()
    {
        healthSlider_.value = GM_.playerCharacter_.GetComponent<Health>().currentHealthPercentage;
        coinText_.text = GM_.playerCharacter_.coin_.ToString();
    }

    private void SwitchUIState(GameUIState gameUIState)
    {
        pause_UI_.SetActive(false);
        gameOver_UI_.SetActive(false);
        gameIsFinished_UI_.SetActive(false);

        Time.timeScale = 1;

        switch(gameUIState)
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
        if(gameUICurrState == GameUIState.Gameplay)
        {
            SwitchUIState(GameUIState.Pause);
        }
        else if(gameUICurrState == GameUIState.Pause)
        {
            SwitchUIState(GameUIState.Gameplay);
        }
    }

    #region MainMenu Button and Restart Button - Old Code
    //public void Button_MainMenu()
    //{
    //    GM_.ReturnToMainMenu();
    //}

    //public void Button_Restart()
    //{
    //    GM_.Restart();
    //}
    #endregion
    public void ShowGameOverUI()
    {
        SwitchUIState(GameUIState.GameOver);
    }

    public void ShowGameIsFinishedUI()
    {
        SwitchUIState(GameUIState.GameIsFinished);
    }

}
