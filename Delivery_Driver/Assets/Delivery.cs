using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    // unitialized bool vars have a value of false
    bool hasPackage; 
    [SerializeField] float destroyTimer = 0.5f;
    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);

    SpriteRenderer spriteRenderer;
    private void Start(){
        // getting a component in the start method and storing it in a variable of type spriterenderer
        // now we can use the . operator to access the spriterender's property
        // use this later in the onTriggerEnter2D method
        spriteRenderer = GetComponent<SpriteRenderer>();
    } 

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Warning: You have collided into something!");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Package" && !hasPackage){
            Debug.Log("Package picked up");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            // when the car passes over the package, destroy the package's (other's) game object with a timer of 0.5f.
            // using a serialized field so that we can edit it within the unity interface
            Destroy(other.gameObject, destroyTimer);
        } 
        
        if (other.tag == "Customer" && hasPackage){
            spriteRenderer.color = noPackageColor;
            Debug.Log("Delivered Package.");
            hasPackage = false;            
        }
    }
}
