using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Take Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;

    static AudioPlayer instance;

    void Awake(){
        ManageSingleton();
    }

    private void ManageSingleton(){
        if(instance != null){
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip(){
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip(){
        PlayClip(damageClip, damageVolume);
    }

    private void PlayClip(AudioClip audioclip, float volume){
        if (audioclip != null){
            AudioSource.PlayClipAtPoint(audioclip, Camera.main.transform.position, volume);
        }
    }
}
