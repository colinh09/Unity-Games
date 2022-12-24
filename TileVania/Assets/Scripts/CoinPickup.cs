using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinValue = 100;

    bool collected = false;

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player" && !collected){
            FindObjectOfType<GameSession>().UpdateScore(coinValue);
            collected = true;
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
