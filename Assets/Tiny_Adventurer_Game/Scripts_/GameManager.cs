using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public Game_UI_Manager gameUIManager_;
    [SerializeField] private Game_UI_View gameUIView_;
    [SerializeField] private Game_UI_Controller gameUIController_;
    public Character playerCharacter_;
    private bool gameIsOver_;

    private void Awake()
    {
        playerCharacter_ = GameObject.FindWithTag("Player").GetComponent<Character>();
    }

    private void Start()
    {
        gameUIView_ = GameObject.Find("Game Canvas").GetComponent<Game_UI_View>();
        gameUIController_ = GameObject.Find("Game UI Controller").GetComponent<Game_UI_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameIsOver_)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //gameUIManager_.TogglePauseUI();
            gameUIView_.TogglePauseUI();
        }

        if(playerCharacter_.currentState_ == Character.CharacterState.Dead)
        {
            gameIsOver_ = true;
            GameOver();
        }
    }

    private void GameOver()
    {
        //gameUIManager_.ShowGameOverUI();
        gameUIController_.ShowGameOverUI();
    }

    public void GameIsFinished()
    {
        //gameUIManager_.ShowGameIsFinishedUI();
        gameUIController_.ShowGameIsFinishedUI();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
