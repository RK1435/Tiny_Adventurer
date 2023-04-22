using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu_UI_Manager : MonoBehaviour
{
    //Buttons
    [SerializeField] private UnityEngine.UI.Button startButton_;
    [SerializeField] private UnityEngine.UI.Button quitButton_;

    private void Awake()
    {
        startButton_.onClick.AddListener(ButtonStart);
        quitButton_.onClick.AddListener(ButtonQuit);
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonQuit()
    {
      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
      #endif

        Application.Quit();
    }
}
