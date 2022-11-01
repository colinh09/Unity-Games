using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CrashDetector : MonoBehaviour
{
    [SerializeField] float delayTimer = 0.5f;
    [SerializeField] ParticleSystem crashEffect;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground"){
            crashEffect.Play();
            Invoke("ReloadScene", delayTimer);
        }
    }
    
    void ReloadScene(){
        SceneManager.LoadScene(0);        
    }
}