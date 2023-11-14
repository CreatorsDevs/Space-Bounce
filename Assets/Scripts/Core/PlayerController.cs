using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(50)]

public class PlayerController : MonoBehaviour
{
    public bool isMovingRight = false;
    public bool isScreenPressed = false;
    public Rigidbody sphereRb;
    public static bool gameOver;
    public static bool isGameActive;
    public float horizontalSpeed = 325.0f;
    void Start()
    {
        sphereRb = GetComponent<Rigidbody>();
        gameOver = false;
        isGameActive = false;
    }
    void FixedUpdate() 
    {
        
    }
    void Update()
    {


        if(Input.GetMouseButtonDown(0) && !isScreenPressed && isGameActive)
        {
            isScreenPressed = true;
        }

        if (isScreenPressed && !isMovingRight)
        {
            sphereRb.velocity = new Vector3(horizontalSpeed * Time.fixedDeltaTime, 0, 0);
            isMovingRight = true;  
            isScreenPressed = false;
            //FindObjectOfType<AudioManager>().Play("Bounce");
            AudioManager.instance.Play("Bounce");
        }
        else if (isScreenPressed && isMovingRight)
        {
            sphereRb.velocity = new Vector3(-horizontalSpeed * Time.fixedDeltaTime, 0, 0);
            isMovingRight = false;
            isScreenPressed = false;
            //FindObjectOfType<AudioManager>().Play("Bounce");
            AudioManager.instance.Play("Bounce");
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Right Wall"))
        {
            isMovingRight = false; // changeVelocityOnCollisionStay() opposite to usual functionality results due to this assignment.
            changeVelocityOnCollisionStay();
            //FindObjectOfType<AudioManager>().Play("Bounce");
            AudioManager.instance.Play("Bounce");
        }
        else if (other.gameObject.CompareTag("Left Wall"))
        {
            isMovingRight = true; // changeVelocityOnCollisionStay() opposite to usual functionality results due to this assignment.
            changeVelocityOnCollisionStay();
            //FindObjectOfType<AudioManager>().Play("Bounce");
            AudioManager.instance.Play("Bounce");
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        if (other.gameObject.CompareTag("Right Wall"))
        {
            isMovingRight = false; // changeVelocityOnCollisionStay() opposite to usual functionality results due to this assignment.
            changeVelocityOnCollisionStay();
            
        }
        else if (other.gameObject.CompareTag("Left Wall"))
        {
            isMovingRight = true; // changeVelocityOnCollisionStay() opposite to usual functionality results due to this assignment.
            changeVelocityOnCollisionStay();
            
        }
    }

    void changeVelocityOnCollisionStay()
    {
        if (!isMovingRight) // It means the movement was hindered due to collision with right wall. Thus, sphere wants to go left now.
        {
            sphereRb.velocity = new Vector3(-horizontalSpeed * Time.fixedDeltaTime, 0, 0); //Since sphere collided with right wall, therefore, it should be moving left now.
            //Debug.Log(horizontalSpeed*Time.deltaTime);
            
        }
         else if (isMovingRight) // It means the movement was hindered due to collision with left wall. Thus, sphere wants to go right now.
        {
            sphereRb.velocity = new Vector3(horizontalSpeed * Time.fixedDeltaTime, 0, 0); //Since sphere collided with left wall, therefore, it should be moving right now.
            //Debug.Log(horizontalSpeed*Time.deltaTime);
            
        }
    }
}