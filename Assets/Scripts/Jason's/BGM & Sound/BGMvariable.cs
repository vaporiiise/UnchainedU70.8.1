using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BGMvariable : MonoBehaviour
{
    [Header("Music Clips")]
    public AudioClip initialBossMusic;
    public AudioClip intenseBossMusic;

    [Header("References")]
    public bossAI bossScript;           // Ref to  boss AI script
    public playerAttack playerScript;   // Ref to player attack script

    [Header("AudioSource")]
    public AudioSource audioSourceA;    // the chill one
    public AudioSource audioSourceB;    // the intense one

    [Header("Health Thresholds")]
    public int lowBossHealth = 50;
    public int lowPlayerHealth = 30;

    private AudioSource activeAudioSource; // Keeps track of which source is currently active
    private bool isInitialMusicPlaying = false;

    private void Start()
    {
        if (audioSourceA == null || audioSourceB == null)
        {
            Debug.LogError("Both AudioSources must be assigned!");
            return;
        }

        if (bossScript == null)
        {
            Debug.LogError("Boss Script is not assigned to the BGMManager!");
            return;
        }

        if (playerScript == null)
        {
            Debug.LogError("Player Script is not assigned to the BGMManager!");
            return;
        }

        audioSourceA.loop = true;
        audioSourceB.loop = true;

        PlayMusic(initialBossMusic, true);
        isInitialMusicPlaying = true;
    }

    private void Update()
    {
        if (bossScript == null || playerScript == null)
            return;

        // Check if boss health is low
        if (bossScript.currentHealth <= lowBossHealth && activeAudioSource.clip != intenseBossMusic)
        {
            PlayMusic(intenseBossMusic, true);
        }
        // Check if player health is low
        else if (playerScript.currentHealth <= lowPlayerHealth && activeAudioSource.clip != intenseBossMusic)
        {
            PlayMusic(intenseBossMusic, true);
        }
        // If neither the boss nor player are low health, keep playing initial music
        else if (activeAudioSource.clip != initialBossMusic && !isInitialMusicPlaying)
        {
            PlayMusic(initialBossMusic, true);
        }
    }

    private void PlayMusic(AudioClip clip, bool loop)
    {
        if (clip == null || (activeAudioSource != null && activeAudioSource.clip == clip))
            return;

        AudioSource newAudioSource = (activeAudioSource == audioSourceA) ? audioSourceB : audioSourceA;

        StartCoroutine(CrossfadeMusic(activeAudioSource, newAudioSource, clip, loop, 1f));

        activeAudioSource = newAudioSource;
    }

    private IEnumerator CrossfadeMusic(AudioSource fromSource, AudioSource toSource, AudioClip newClip, bool loop, float fadeDuration)
    {
        float startVolume = fromSource != null ? fromSource.volume : 1f;
        float elapsed = 0f;

        toSource.clip = newClip;
        toSource.loop = loop;
        toSource.volume = 0;
        toSource.Play();

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            if (fromSource != null)
                fromSource.volume = Mathf.Lerp(startVolume, 0, t);

            toSource.volume = Mathf.Lerp(0, 1, t);

            yield return null;
        }

        if (fromSource != null)
        {
            fromSource.Stop();
            fromSource.volume = startVolume;
        }
        toSource.volume = 1;
    }
}
