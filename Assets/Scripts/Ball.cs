//============================================================
// Author: Isaac Shields
// Date  : 11-24-2024
// Desc  : Controls ball movement
//============================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Ball : MonoBehaviour
{
    private Rigidbody2D ballRigidBody;
    public GameObject paddle;
    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameMaster gm;
    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //launches the ball
        if(Input.GetMouseButtonDown(0) && !gm.ballIsMoving && !gm.isPaused)
        {
            Time.timeScale = 1;

            ballRigidBody = GetComponent<Rigidbody2D>();
            gm.ballIsMoving = true;
            ballRigidBody.simulated = true;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z)));
            mousePosition.x = Mathf.Clamp(mousePosition.x, leftWall.transform.position.x, rightWall.transform.position.x);
            mousePosition.y = Mathf.Clamp(mousePosition.y, paddle.transform.position.y + 1, topWall.transform.position.y);
            Vector2 direction = (new Vector2(mousePosition.x, mousePosition.y) - ballRigidBody.position).normalized;
            ballRigidBody.velocity = direction * gm.ballSpeed;
        }
    }

    private void FixedUpdate() 
    {
        if(gm.ballIsMoving && ballRigidBody.velocity.magnitude != gm.ballSpeed)
        {
            ballRigidBody.velocity = ballRigidBody.velocity.normalized * gm.ballSpeed;
        }
    }

    //handles out of bounds
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject == bottomWall)
        {
            paddle.transform.position = new Vector2(0, paddle.transform.position.y);
            ballRigidBody.simulated = false;
            gm.ballIsMoving = false;
            gameObject.transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + gameObject.transform.lossyScale.y, paddle.transform.position.z);
            gm.removeHeart();
        }
    }

    //handles resetting the ball
    public void resetItems()
    {
        paddle.transform.position = new Vector2(0, paddle.transform.position.y);
        ballRigidBody.simulated = false;
        gm.ballIsMoving = false;
        gameObject.transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + gameObject.transform.lossyScale.y, paddle.transform.position.z);
    }

}
