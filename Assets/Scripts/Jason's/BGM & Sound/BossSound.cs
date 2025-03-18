using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosssound : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource idleSource;
    public AudioSource walkingSource;
    public AudioSource growlingSource;
    public AudioSource attackSource;
    public AudioSource groundPoundSource;
    public AudioSource jumpSource;
    public AudioSource landingSource;
    public AudioSource hurtSource;


    [Header("Audio Clips")]
    public AudioClip[] idleClips;
    public AudioClip[] walkingClips;
    public AudioClip[] growlingClips;
    public AudioClip[] attackClips;
    public AudioClip[] groundPoundClips;
    public AudioClip[] jumpClips;
    public AudioClip[] landingClips;
    public AudioClip[] hurtClips;


    [Header("Idle Sound Settings")]
    public float idleSoundInterval = 5f;
    private float idleTimer;

    public Animator bossAnim;
    public int currentHealth;
    public HealthBar healthBar;
    public GameObject HealthBar;

    void Update()
    {
        HandleIdleSounds();
    }
    private void HandleIdleSounds()
    {
        if (idleClips.Length == 0 || idleSource.isPlaying) return;

        idleTimer += Time.deltaTime;
        if (idleTimer >= idleSoundInterval)
        {
            PlayRandomSound(idleSource, idleClips);
            idleTimer = 0f;
        }
    }

    public void PlayWalkingSound()
    {
        PlayRandomSound(walkingSource, walkingClips);
    }

    public void PlayGrowlingSound()
    {
        PlayRandomSound(growlingSource, growlingClips);
    }

    public void PlayAttackSound()
    {
        PlayRandomSound(attackSource, attackClips);
    }
    public void PlayGroundPoundSound()
    {
        PlayRandomSound(groundPoundSource, groundPoundClips);
    }

    public void PlayJumpSound()
    {
        PlayRandomSound(jumpSource, jumpClips);
    }
    public void PlayLandingSound()
    {
        PlayRandomSound(landingSource, landingClips);
    }

    public void PlayHurtSound()
    {
        PlayRandomSound(hurtSource, hurtClips);
    }

    private void PlayRandomSound(AudioSource source, AudioClip[] clips)
    {
        if (clips.Length == 0) return;

        int randomIndex = Random.Range(0, clips.Length);
        source.clip = clips[randomIndex];
        source.Play();
    }
}
