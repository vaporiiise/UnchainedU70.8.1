using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NHPlayAudioOnDestroy : MonoBehaviour
{
    public AudioClip destructionSound; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnDestroy()
    {
        if (CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(destructionSound);
            Debug.Log("Enemy destroyed, playing sound.");
        }
    }
}
