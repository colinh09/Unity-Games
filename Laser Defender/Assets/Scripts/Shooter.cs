using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{   
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool isAI;
    [SerializeField] float minimumShootingSpeed = 0.05f;
    [SerializeField] float fireVariance = 0.3f;

    Coroutine firingCoroutine;
    [HideInInspector] public bool isFiring;

    AudioPlayer audioPlayer;

    void Awake(){
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (isAI){
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    private void Fire(){
        if (isFiring && firingCoroutine == null){
            firingCoroutine = StartCoroutine(FireContinously());
        } else if (!isFiring && firingCoroutine != null){
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireContinously(){
        while(true){
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null){
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(GetRandomSpawnTime());
        }
    }

    private float GetRandomSpawnTime(){
        float spawnTime = Random.Range(firingRate - fireVariance, firingRate + fireVariance);
        return Mathf.Clamp(spawnTime, minimumShootingSpeed, float.MaxValue);
    }
}
