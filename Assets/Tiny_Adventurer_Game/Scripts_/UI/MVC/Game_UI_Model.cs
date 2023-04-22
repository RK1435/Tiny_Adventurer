using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class Game_UI_Model
{
    #region MVC - Model
    private Game_UI_Controller gameUIController_;

    public Game_UI_Model() { }

    public void SetGameUIController(Game_UI_Controller gameUIController)
    {
        gameUIController_ = gameUIController;
    }
    #endregion

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


}


