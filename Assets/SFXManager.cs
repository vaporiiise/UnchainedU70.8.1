using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioMixer audioMixer; // Optional: Assign this in Unity for volume control

    private List<AudioSource> bgmSources = new List<AudioSource>();  // Stores all BGM sources
    private List<AudioSource> sfxSources = new List<AudioSource>();  // Stores all SFX sources

    private const string VolumeKey = "Volume_";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Find and store all audio sources in the tagged GameObjects
        FindAudioSourcesByTag("BGM", ref bgmSources);
        FindAudioSourcesByTag("SFX", ref sfxSources);

        // Load and apply saved volume settings
        float masterVolume = PlayerPrefs.GetInt(VolumeKey + "Master", 10) / 10f;
        float bgmVolume = PlayerPrefs.GetInt(VolumeKey + "BGM", 10) / 10f;
        float sfxVolume = PlayerPrefs.GetInt(VolumeKey + "SFX", 10) / 10f;

        SetVolumeByTag("Master", masterVolume);
        SetVolumeByTag("BGM", bgmVolume);
        SetVolumeByTag("SFX", sfxVolume);
    }

    private void FindAudioSourcesByTag(string tag, ref List<AudioSource> audioSourcesList)
    {
        // Find GameObject by tag
        GameObject audioContainer = GameObject.FindGameObjectWithTag(tag);
        if (audioContainer != null)
        {
            // Get all AudioSources inside that GameObject
            audioSourcesList.AddRange(audioContainer.GetComponentsInChildren<AudioSource>());
        }
        else
        {
            Debug.LogError($"AudioManager: No GameObject found with tag '{tag}'");
        }
    }

    public void SetVolumeByTag(string tag, float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat(tag, Mathf.Log10(volume) * 20); // Convert linear 0-1 to dB
        }

        if (tag == "Master")
        {
            AudioListener.volume = volume;
        }
        else if (tag == "SFX")
        {
            foreach (var source in sfxSources)
            {
                source.volume = volume;
            }
            Debug.Log($"SFX Volume Set: {volume}");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSources.Count == 0) return;

        // Find an available (not playing) AudioSource, or use the first one
        foreach (var source in sfxSources)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(clip);
                return;
            }
        }

        // If all sources are busy, play on the first one (fallback)
        sfxSources[0].PlayOneShot(clip);
    }

    public AudioSource GetSFXSource()
    {
        if (sfxSources.Count > 0)
        {
            return sfxSources[0]; // Return the first SFX source
        }
        return null;
    }
}
