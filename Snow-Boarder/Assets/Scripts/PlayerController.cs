using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float boostSpeed = 100f;
    [SerializeField] float baseSpeed = 15f;
    SurfaceEffector2D surfaceEffector2D;
    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // this lets us access the surface effector that is in the level sprite
        surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update(){  
        if (canMove){ 
            RotatePlayer();
            RespondToBoost();
        }
    }

    public void DisableControls(){
        canMove = false;
    }

    void RespondToBoost(){
        // if we push up, speed up, otherwise stay at normal speed
        if(Input.GetKey(KeyCode.UpArrow)){
            surfaceEffector2D.speed = boostSpeed;
        } else {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void RotatePlayer(){
        // using old input method for simplicity
        // if the player presses the left key, we want the snowboarder to spin 
        // we allow the snowboarder to rotate by adding torque to its rigid body
        if(Input.GetKey(KeyCode.LeftArrow)){
            rb2d.AddTorque(torqueAmount);
        }
        else if(Input.GetKey(KeyCode.RightArrow)){
            rb2d.AddTorque(-torqueAmount);
        }              
    }
}
