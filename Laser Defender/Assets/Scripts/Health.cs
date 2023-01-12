using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    [SerializeField] bool isPlayer;
    [SerializeField] int scoreValue = 100;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake(){
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public int GetHealth(){
        return health;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer){
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
            audioPlayer.PlayDamageClip();
        }
    }

    private void TakeDamage(int damage){
        health -= damage;
        if (health <= 0){
            if (!isPlayer){
                scoreKeeper.ModifyScore(scoreValue);
            } else {
                levelManager.LoadGameOver();
            }
            Destroy(gameObject);
        }
    }

    private void PlayHitEffect(){
        if(hitEffect != null){
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera(){
        if(cameraShake != null && applyCameraShake){
            cameraShake.Play();
        }
    }
}
