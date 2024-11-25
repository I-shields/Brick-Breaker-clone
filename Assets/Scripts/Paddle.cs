//============================================================
// Author: Isaac Shields
// Date  : 11-24-2024
// Desc  : Handles paddle movement
//============================================================
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Paddle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject paddle;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameMaster gm;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(gm.ballIsMoving && !gm.gameOver && !gm.isPaused)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            mousePosition.x = Mathf.Clamp(mousePosition.x, leftWall.transform.position.x + paddle.transform.lossyScale.y, rightWall.transform.position.x - paddle.transform.lossyScale.y);
            paddle.transform.position = new Vector3(-mousePosition.x, paddle.transform.position.y, 0);
        }
    }

    //add some effects to the player
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "heartDrop")
        {
            gm.addHeartsInGame();
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "iceDrop")
        {
            gm.removeHeart();
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "coinDrop")
        {
            gm.playerScore += 1000;
            Destroy(other.gameObject);
        }
    }

    //move home when game is done
    public void resetPos()
    {
        paddle.transform.position = new Vector3(0, paddle.transform.position.y, 0);
    }
}
