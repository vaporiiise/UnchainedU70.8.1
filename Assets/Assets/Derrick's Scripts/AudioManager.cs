using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private Dictionary<string, float> volumeLevels = new Dictionary<string, float>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevents destruction on scene reload
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        // Load saved volume levels
        LoadVolume("BGM");
        LoadVolume("SFX");

        // Apply volumes to all tagged AudioSources
        ApplyVolumeToTag("BGM");
        ApplyVolumeToTag("SFX");
    }

    private void LoadVolume(string tag)
    {
        volumeLevels[tag] = PlayerPrefs.GetFloat("Volume_" + tag, 1f); // Default to full volume (1.0)
    }

    public float GetVolumeByTag(string tag)
    {
        return volumeLevels.ContainsKey(tag) ? volumeLevels[tag] : 1f;
    }

    public void SetVolumeByTag(string tag, float volume)
    {
        volumeLevels[tag] = volume;
        PlayerPrefs.SetFloat("Volume_" + tag, volume);
        PlayerPrefs.Save();

        ApplyVolumeToTag(tag); // Apply volume change immediately
    }

    private void ApplyVolumeToTag(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in taggedObjects)
        {
            AudioSource source = obj.GetComponent<AudioSource>();
            if (source != null)
            {
                source.volume = volumeLevels[tag]; // Apply saved volume
            }
        }
    }
}