using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button mStartButton, mExitButton;
    private void Awake()
    {
        mStartButton.onClick.AddListener(startGameAction);
        mExitButton.onClick.AddListener(exitAction);
    }
    
    public void startGameAction()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }
    public void exitAction()
    {
        Application.Quit();
    }
    
}
