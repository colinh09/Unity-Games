using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishLine : MonoBehaviour
{
    [SerializeField] float delayTimer = 1f;
    [SerializeField] ParticleSystem finishEffect;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            finishEffect.Play();
            // delay the reloading of the scene after winning the game
            Invoke("ReloadScene", delayTimer);
        }
    }

    void ReloadScene(){
        // scene management class to use load scene method
        // add index of scene as a parameter to loadscene method
        // this makes so when the player crosses (triggers) the finish line, the player gets sent back to the start
        // scenemanager is just reloading the scene upon trigger        
        SceneManager.LoadScene(0);
    }
}
