//============================================================
// Author: Isaac Shields
// Date  : 11-24-2024
// Desc  : Master script
//============================================================
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool ballIsMoving;
    public int playerLives = 3;
    public float ballSpeed;
    public List<GameObject> blocks;
    public GameObject healthSpawn;
    public GameObject iceSpawn;
    public GameObject pointsSpawn;
    public GameObject pauseMenu;
    public GameObject mainGameCanvas;
    public bool isPaused;
    public TextMeshProUGUI scoreBox;
    public int playerScore;
    public bool gameLoss;
    public bool gameOver;
    public int level;
    public GameObject brickType1;
    public GameObject spawnPos;
    private Vector2 spawnPosVec;
    private List<GameObject> hearts = new List<GameObject>();
    public GameObject heartPrefab;
    public Ball ball;
    public Paddle paddle;
    public LayerMask dropLayer;
    private int baseLives;

    private void Start() 
    {
        spawnPosVec = spawnPos.transform.position;
        ballIsMoving = false;
        isPaused = false;
        playerScore = 0;
        gameOver = false;
        gameLoss = false;
        createHealth();
        level = Random.Range(0,3);
        startGame(level);
        blocks = GameObject.FindGameObjectsWithTag("Blocks").ToList<GameObject>();
        baseLives = playerLives;

    }

    private void Update() 
    {
        //handles death
        if(playerLives == 0)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            pauseMenu.GetComponent<FinalScreen>().initCanvas();
            mainGameCanvas.SetActive(false);
            gameLoss = true;
            gameOver = true;
        }
        //handles wins
        if(blocks.Count == 0)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            pauseMenu.GetComponent<FinalScreen>().initCanvas();
            mainGameCanvas.SetActive(false);
            gameOver = true;
            //start endgame
        }
        //pauses game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }

        scoreBox.text = "Score: " + playerScore.ToString();
    }

    public void spawnHealth(Vector3 spawnPos)
    {
        Instantiate(healthSpawn, spawnPos, Quaternion.identity);
    }

    public void spawnIce(Vector3 spawnPos)
    {
        Instantiate(iceSpawn, spawnPos, Quaternion.identity);
    }

    public void spawnPoints(Vector3 spawnPos)
    {
        Instantiate(pointsSpawn, spawnPos, Quaternion.identity);
    }

    public void pauseGame()
    {
        if(!isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            pauseMenu.GetComponent<FinalScreen>().initCanvas();
            mainGameCanvas.SetActive(false);
            isPaused = true;
        }
        else
        {
            if(gameOver)
            {
                restartGame();
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                mainGameCanvas.SetActive(true);
                isPaused = false;
            }
        }
    }

    //create level layout depending on level number
    public void startGame(int level)
    {
        GameObject temp;
        spawnPosVec = spawnPos.transform.position;
        if(level == 1)
        {
            spawnPosVec = new Vector2(spawnPosVec.x - brickType1.transform.lossyScale.x * 2.5f, spawnPosVec.y);
            for(int a = 0; a < 4; a++)
            {
                for(int i = 0; i < 4; i++)
                {
                    temp = GameObject.Instantiate(brickType1, spawnPosVec, Quaternion.identity);
                    temp.GetComponent<Bricks>().brickType = Random.Range(-5, 5);
                    temp.GetComponent<Bricks>().updateSprite();

                    Vector2 tempPos = spawnPosVec;

                    for(int j = 0; j < 5; j++)
                    {
                        tempPos.y = tempPos.y + brickType1.transform.lossyScale.y;
                        temp = GameObject.Instantiate(brickType1, tempPos, Quaternion.identity);
                        temp.GetComponent<Bricks>().brickType = Random.Range(-5, 5);
                        temp.GetComponent<Bricks>().updateSprite();

                    }

                    spawnPosVec = new Vector2(spawnPosVec.x + brickType1.transform.lossyScale.x, spawnPosVec.y);

                }

                spawnPosVec = new Vector2(spawnPosVec.x + brickType1.transform.lossyScale.x * 2, spawnPosVec.y);
            }
        }

        else if(level == 2)
        {
            //vertical
            for (int a = 1; a < 4; a++)
            {
                for(int i = 0; i < 16; i++)
                {
                    temp = Instantiate(brickType1, spawnPosVec, Quaternion.identity);
                    temp.GetComponent<Bricks>().brickType = Random.Range(-5, 5);
                    temp.GetComponent<Bricks>().updateSprite();
                    spawnPosVec = new Vector2(spawnPosVec.x + brickType1.transform.lossyScale.x, spawnPosVec.y);
                }

                spawnPosVec = new Vector2(spawnPos.transform.position.x, spawnPos.transform.position.y + brickType1.transform.lossyScale.y * 2*a);

            }
        }


        else if(level != 1 || level != 2)
        {
            //block
            for(int i = 0; i < 16; i++)
            {
                temp = Instantiate(brickType1, spawnPosVec, Quaternion.identity);
                temp.GetComponent<Bricks>().brickType = Random.Range(-5, 5);
                temp.GetComponent<Bricks>().updateSprite();
                Vector2 tempPos = spawnPosVec;
                spawnPosVec = new Vector2(spawnPosVec.x + brickType1.transform.lossyScale.x, spawnPosVec.y);
                for(int j = 0; j < 4; j++)
                {
                    tempPos = new Vector2(tempPos.x, tempPos.y + brickType1.transform.lossyScale.y);
                    temp = Instantiate(brickType1, tempPos, Quaternion.identity);
                    temp.GetComponent<Bricks>().brickType = Random.Range(-5, 5);
                    temp.GetComponent<Bricks>().updateSprite();
                }
            }
        }
    }

    public void restartGame()
    {
        blocks = GameObject.FindGameObjectsWithTag("Blocks").ToList<GameObject>();

        foreach(GameObject block in blocks)
        {
            Destroy(block);
        }

        GameObject[] allItems = FindObjectsOfType<GameObject>();

        foreach (GameObject item in allItems)
        {
            if(item.layer == dropLayer)
            {
                Destroy(item);
            }
        }

        playerLives = 3;
        playerScore = 0;
        gameOver = false;
        gameLoss = false;
        ball.resetItems();
        paddle.resetPos();
        spawnPosVec = spawnPos.transform.position;
        createHealth();
        startGame(level);
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        mainGameCanvas.SetActive(true);
        isPaused = false;
        blocks = GameObject.FindGameObjectsWithTag("Blocks").ToList<GameObject>();
    }

    private void createHealth()
    {
        foreach(GameObject heart in GameObject.FindGameObjectsWithTag("Heart"))
        {
            Destroy(heart);
        }

        //adds the heart images to the ui
        for(int i = 0; i < playerLives; i++)
        {
            GameObject temp = Instantiate(heartPrefab);
            temp.transform.SetParent(mainGameCanvas.transform, false);
            temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200 + (75 * i), -50);
            hearts.Add(temp);
        }
    }

    public void removeHeart()
    {
        playerLives--;
        Destroy(hearts[hearts.Count-1]);
        hearts.RemoveAt(hearts.Count - 1);
    }

    public void addHeartsInGame()
    {
        //add heart to ui when picked up if the player needs it
        if(hearts.Count < baseLives)
        {
            float lastHeartPos = hearts[hearts.Count - 1].transform.localPosition.x;
            GameObject newHeart = Instantiate(heartPrefab);
            newHeart.transform.SetParent(mainGameCanvas.transform, false);
            newHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200 + (75 * hearts.Count), -50);
            hearts.Add(newHeart);
            playerLives++;
        }
        else
        {
            playerScore += 100;
        }
    }
    
}
