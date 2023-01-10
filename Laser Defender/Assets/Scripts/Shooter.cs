using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{   
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.1f;

    Coroutine firingCoroutine;
    public bool isFiring;

    void Start()
    {

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
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null){
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance, projectileLifetime);
            yield return new WaitForSeconds(firingRate);
    }
}
