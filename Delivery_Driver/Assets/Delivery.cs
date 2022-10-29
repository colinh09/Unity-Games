using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    // unitialized bool vars have a value of false
    bool hasPackage; 
    [SerializeField] float destroyTimer = 0.5f;
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Warning: You have collided into something!");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Package" && !hasPackage){
            Debug.Log("Package picked up");
            hasPackage = true;
            // when the car passes over the package, destroy the package's (other's) game object with a timer of 0.5f.
            // using a serialized field so that we can edit it within the unity interface
            Destroy(other.gameObject, destroyTimer);
        } 
        
        if (other.tag == "Customer" && hasPackage){
            Debug.Log("Delivered Package.");
            hasPackage = false;            
        }
    }
}
