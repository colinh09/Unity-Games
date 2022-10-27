using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    // SerializeField allows you to change the value of a variable within the unity editor interface
    [SerializeField] float steerSpeed = 1f;
    [SerializeField] float moveSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // we know which direction the car is driving by using the z-axis of the car
        // use unity's build in transform.rotate() method to handle this
        // rotate takes 3 params, how much the x, y, and z axis are changing
        // need to include the f at the end so that unity knows that 0.1 is a float value
        transform.Rotate(0, 0, steerSpeed); 

        // transform also has a translate method, same idea. 
        transform.Translate(0, moveSpeed, 0);
    }
}
