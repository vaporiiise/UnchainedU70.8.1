using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeaterManager : MonoBehaviour
{
    [Header("References")]
    public BHPlayerHeaalth playerHealth;  // Drag and drop the Player Health script
    public AudioSource audioSource;  // Drag and drop an AudioSource

    [Header("Health Thresholds")]
    public int lowHealthThreshold = 20;  // Slow heartbeat plays below this health
    public int criticalHealthThreshold = 10; // Fast heartbeat plays below this health

    [Header("Heartbeat Sounds")]
    public AudioClip slowHeartbeatClip;
    public AudioClip fastHeartbeatClip;

    [Header("Heartbeat Speeds")]
    public float slowHeartbeatSpeed = 1f;
    public float fastHeartbeatSpeed = 1.5f;

    private bool isSlowPlaying = false;
    private bool isFastPlaying = false;

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource assigned!");
            return;
        }

        if (slowHeartbeatClip == null && fastHeartbeatClip == null)
        {
            Debug.LogError("No heartbeat sounds assigned!");
            return;
        }

        audioSource.loop = true;
        audioSource.playOnAwake = false;

        Debug.Log("Testing audio play...");
        audioSource.clip = slowHeartbeatClip != null ? slowHeartbeatClip : fastHeartbeatClip;
        audioSource.Play(); // If this doesn't play, the issue is with the AudioSource setup.
    }

    private void Update()
    {
        if (playerHealth == null || audioSource == null)
        {
            return; // Prevents errors if components are missing
        }

        int currentHealth = playerHealth.GetCurrentHealth();
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth >= lowHealthThreshold)
        {
            StopHeartbeat();
        }
        else if (currentHealth > criticalHealthThreshold)
        {
            PlayHeartbeat(slowHeartbeatClip, ref isSlowPlaying, ref isFastPlaying, slowHeartbeatSpeed);
        }
        else if (currentHealth > 0)
        {
            PlayHeartbeat(fastHeartbeatClip, ref isFastPlaying, ref isSlowPlaying, fastHeartbeatSpeed);
        }
        else
        {
            StopHeartbeat();
        }
    }

    private void PlayHeartbeat(AudioClip clip, ref bool isPlaying, ref bool stopOther, float pitch)
    {
        if (clip == null)
        {
            Debug.LogWarning("HeartbeatController: No heartbeat clip assigned!");
            return;
        }

        if (!isPlaying || audioSource.clip != clip)
        {
            Debug.Log("Playing heartbeat: " + clip.name);
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.pitch = pitch;
            audioSource.Play();
            isPlaying = true;
            stopOther = false;
        }
    }

    private void StopHeartbeat()
    {
        if (audioSource.isPlaying)
        {
            Debug.Log("Stopping heartbeat.");
            audioSource.Stop();
            isSlowPlaying = false;
            isFastPlaying = false;
        }
    }
}
