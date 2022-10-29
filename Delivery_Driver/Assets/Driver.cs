using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    // SerializeField allows you to change the value of a variable within the unity editor interface
    [SerializeField] float steerSpeed = 50f;
    [SerializeField] float moveSpeed = 10f;

    [SerializeField] float slowSpeed = 10f;
    [SerializeField] float boostSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        // there are keyboard inputs already set up in Unity
        // by using Input.GetAxis, we can manipulate how key 
        // inputs from the user will affect our car's translations and rotations

        // need to multiply by Time.deltaTime to ensure that no matter the speed of the computer, the number of
        // actions performed per frame will be constant. Don't really need to understand the details of it too much
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        // we know which direction the car is driving by using the z-axis of the car
        // use unity's build in transform.rotate() method to handle this
        // rotate takes 3 params, how much the x, y, and z axis are changing
        // need to include the f at the end so that unity knows that 0.1 is a float value
        transform.Rotate(0, 0, -steerAmount); 

        // transform also has a translate method, same idea. 
        transform.Translate(0, moveAmount, 0);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        moveSpeed = slowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "speedBoost"){
            moveSpeed = boostSpeed;
        }
    }

}

