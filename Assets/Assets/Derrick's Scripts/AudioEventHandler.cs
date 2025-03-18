using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventHandler : MonoBehaviour
{
    public AudioSource audioSource; 

    public void PlaySound()
    {
            audioSource.Play();
    }
}
