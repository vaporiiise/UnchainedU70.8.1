using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuidoiSequencePlayer : MonoBehaviour
{
    public AudioSource[] audioSources;
    private int currentAudioIndex = 0;

    void Start()
    {
        if (audioSources.Length > 0)
        {
            PlayAudio(currentAudioIndex);
        }
    }

    void PlayAudio(int index)
    {
        if (index >= audioSources.Length) return;

        audioSources[index].Play();
        StartCoroutine(WaitForAudioToEnd(audioSources[index]));
    }

    IEnumerator WaitForAudioToEnd(AudioSource audioSource)
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        currentAudioIndex++;
        if (currentAudioIndex < audioSources.Length)
        {
            PlayAudio(currentAudioIndex);
        }
    }
}