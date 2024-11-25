//============================================================
// Author: Isaac Shields
// Date  : 11-24-2024
// Desc  : Aim line thingy
//============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiming : MonoBehaviour
{
    public Transform ball;
    private LineRenderer lr;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject topWall;
    public GameObject paddle;
    public GameMaster gm;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        //aim
        if(!gm.ballIsMoving && !gm.isPaused && !gm.gameOver)
        {
            lr.enabled = true;
            lr.SetPosition(0, ball.position);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            mousePosition.x = Mathf.Clamp(mousePosition.x, leftWall.transform.position.x, rightWall.transform.position.x);
            mousePosition.y = Mathf.Clamp(mousePosition.y, paddle.transform.position.y + 1, topWall.transform.position.y);
            mousePosition.z = ball.position.z;

            lr.SetPosition(1, -mousePosition);
        }

        if(gm.ballIsMoving || gm.isPaused || gm.gameOver)
        {
            lr.enabled = false;
        }
            
    }
}
