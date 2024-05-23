using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool manual;
    private bool hasBall;
    [SerializeField] int playerIndex;
    [SerializeField] GameObject throwLine; 
    [SerializeField] Ball ball1;
    [SerializeField] Ball ball2;
    [SerializeField] Ball ball3;
    [SerializeField] float grabDistance = 1.5f;
    private Ball currentBall;

    private Vector3 lastPosition;
    private Vector3 playerVelocity;
    private bool isRecording;

    void Start()
    {
        //ball.thrown = false;

        hasBall = false;
        currentBall = null;

        lastPosition = transform.position;
        isRecording = false;
    }

    void Update()
    {
        Vector3 playerPos = transform.position; //position of the player

        // Start the coroutine to update lastPosition if not already running
        if (!isRecording)
        {
            StartCoroutine(UpdateLastPositionWithDelay());
        }

        // Calculate player velocity based on lastPosition
        Vector3 playerVelocity = (playerPos - lastPosition) / 1f; // 1 second delay

        if (hasBall == false)
        {
            CheckAndGrabBall(playerPos, ball1);
            CheckAndGrabBall(playerPos, ball2);
            CheckAndGrabBall(playerPos, ball3);
        }

        if (hasBall == true && currentBall != null)
        {
            currentBall.SetPosition(playerPos);

            if (playerPos.z > throwLine.transform.position.z)
            {
                currentBall.ThrowBall(playerVelocity);
                hasBall = false;
                currentBall = null;
            }
        }

        //// If the ball has not been thrown, move it with the player
        //if (!ball.thrown)
        //{
        //    //ball.SetPosition(playerPos); //set the position of the ball with the player position
        //}

        //// If the player has passed the throw line, throw the ball
        //if (!ball.thrown && playerPos.z > throwLine.transform.position.z)
        //{
        //    ball.ThrowBall();
        //}
    }

    void CheckAndGrabBall(Vector3 playerPos, Ball ball)
    {
        if (ball == null || ball.thrown) return;

        if (Vector3.Distance(playerPos, ball.transform.position) <= grabDistance)
        {
            Debug.Log("Ball grabbed: " + ball.name);
            hasBall = true;
            currentBall = ball;
            currentBall.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator UpdateLastPositionWithDelay()
    {
        isRecording = true;
        yield return new WaitForSeconds(1f); // Wait for 1 second
        lastPosition = transform.position;
        isRecording = false;
    }

    public void setPosition(Vector3 pos)
    {
        Vector3 newPos;
        switch (playerIndex)
        {
            case 1:
                newPos = new Vector3(Mathf.Clamp(pos.x, 0, 50), pos.y, pos.z);
                break;
            case 2:
                newPos = new Vector3(Mathf.Clamp(pos.x, 50, 100), pos.y, pos.z);
                break;
            default:
                newPos = pos;
                break;
        }
        transform.position = newPos;
    }

    public void setRotation(Quaternion quat)
    {
        transform.localRotation = quat;
    }
}
