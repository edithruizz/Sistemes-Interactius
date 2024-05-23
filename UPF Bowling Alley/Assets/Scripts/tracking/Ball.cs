using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    private Rigidbody myBody;
    public bool thrown { get; set; }
    public float horizontalSpeed;
    public Vector3 restPosition;
    private Vector3 initialPosition;
    private int count;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        initialPosition = transform.position; //initial position
        myBody.isKinematic = true;            //can move with player
        count = 0;                            //set the count to zero 
    }

    void Update()
    {
        if (!thrown && myBody.isKinematic)
        {
            BallMovement();
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    void BallMovement()
    {
        float xAxis = Input.GetAxis("Horizontal");
        Vector3 position = transform.position;
        //position.x += xAxis * horizontalSpeed;
        transform.position = position;
    }

    public void ThrowBall(Vector3 playerVelocity)
    {
        thrown = true;
        myBody.isKinematic = false; //it doesn't depend on the player movement
        //myBody.velocity = new Vector3(0, 0, speed);
        myBody.velocity = playerVelocity + new Vector3(0, 0, 1);
    }

    private void FixedUpdate()
    {

    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Pin"))
    //    {
    //        collision.gameObject.SetActive(false);
    //        count++;
    //    }

    //    if (collision.gameObject.CompareTag("End"))
    //    {
    //        ResetBallPosition();
    //    }
    //}

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pin"))
        {
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("End"))
        {
            ResetBallPosition();
        }
    }*/
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            ResetBallPosition();
        }
    }

    void ResetBallPosition()
    {
        thrown = false;
        myBody.isKinematic = true;
        transform.position = restPosition;
        //Debug.Log("Rest Position: " + transform.position);
        myBody.velocity = Vector3.zero;
        //speed = restSpeed;
    }
}