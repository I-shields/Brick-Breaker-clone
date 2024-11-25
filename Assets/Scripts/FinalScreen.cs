//============================================================
// Author: Isaac Shields
// Date  : 11-24-2024
// Desc  : handles pause/end game screen
//============================================================
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScreen : MonoBehaviour
{
    public GameMaster gm;
    public TextMeshProUGUI titleText;
    public Button quantumBtn;
    public Button quitBtn;
    public Button menuBtn;

    private void Start() {
        quitBtn.onClick.AddListener(quitGame);
        quantumBtn.onClick.AddListener(resumeGame);
        menuBtn.onClick.AddListener(returnToMain);
    }

    public void initCanvas()
    {
        //change some text
        if(gm.gameOver && gm.gameLoss)
        {
            quantumBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Restart Game";
            titleText.text = $"You lost, your final score is: \n {gm.playerScore}";
        }

        if(gm.gameOver && !gm.gameLoss)
        {
            quantumBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Restart Game";
            titleText.text = $"You won! your final score is: \n {gm.playerScore}";
        }
    }

    private void quitGame()
    {
        Application.Quit();
    }

    private void resumeGame()
    {
        gm.isPaused = true;
        gm.pauseGame();
    }

    private void returnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
