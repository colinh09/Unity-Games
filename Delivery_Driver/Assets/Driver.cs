using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
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
        transform.Rotate(0, 0, 0.1f);        
    }
}
