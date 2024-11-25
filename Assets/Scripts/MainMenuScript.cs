//============================================================
// Author: Isaac Shields
// Date  : 11-24-2024
// Desc  : Main menu script
//============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button startGameBtn;
    public Button quitGameBtn;

    void Start() 
    {
        startGameBtn.onClick.AddListener(startGame);
        quitGameBtn.onClick.AddListener(quitGame);
    } 

    private void startGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void quitGame()
    {
        Application.Quit();
    }
}
