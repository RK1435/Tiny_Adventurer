using System;
using UnityEngine;

public class Game_UI_Controller : MonoBehaviour
{
    #region MVC - Controller
    [SerializeField] private Game_UI_View gameUIView_;
    private Game_UI_Model gameUIModel_;
    public Game_UI_Controller(Game_UI_Model gameUIModel, Game_UI_View gameUIView)
    {
        gameUIModel_ = gameUIModel;
        gameUIView_ = gameUIView;
        gameUIModel_.SetGameUIController(this);
        gameUIView_.SetGameUIController(this);
    }

    #endregion

    public void ShowGameOverUI()
    {
        //gameUIView_.SwitchUIState(Game_UI_View.GameUIState.GameOver);
        gameUIView_.SwitchUIState(gameUIState: Game_UI_View.GameUIState.GameOver);
    }

    public void ShowGameIsFinishedUI()
    {
        gameUIView_.SwitchUIState(gameUIState: Game_UI_View.GameUIState.GameIsFinished);
    }
}
