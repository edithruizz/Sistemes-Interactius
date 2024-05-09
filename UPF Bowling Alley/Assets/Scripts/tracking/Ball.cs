using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float speed;
    private Rigidbody myBody;
    private bool thrown = false;
    public float horizontalSpeed;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        // Assign the Rigidbody component to our private mybody variable
        myBody = GetComponent<Rigidbody>();

        // Set the count to zero 
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        BallMovement();
    }

    void BallMovement()
    {
        if (!thrown)
        {
            float xAxis = Input.GetAxis("Horizontal");
            Vector3 position = transform.position;
            position.x += xAxis * horizontalSpeed;
            transform.position = position;

        }
        if(!thrown && Input.GetKeyDown(KeyCode.Space))
        {
            thrown = true;
            myBody.isKinematic = false;
            myBody.velocity = new Vector3(0, 0, speed);
        }
    }

    private void FixedUpdate()
    {
        if(thrown && myBody.IsSleeping())
        {
            SceneManager.LoadScene("Scene");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided GameObject is tagged as "Pin"
        if (collision.gameObject.CompareTag("Pin"))
        {
            // Deactivate (disable) the pin GameObject
            collision.gameObject.SetActive(false);

            // Increment the count variable
            count++;
        }
    }
}
