using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioMixer audioMixer;

    private List<AudioSource> bgmSources = new List<AudioSource>();
    private List<AudioSource> sfxSources = new List<AudioSource>();

    private const string VolumeKey = "Volume_";
    private float lastMasterVolume = 1f;

    private void Awake()
    {
        instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        RefreshAudioSources();

        ApplySavedVolumes();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshAudioSources();
        ApplySavedVolumes();
    }

    private void RefreshAudioSources()
    {
        bgmSources.Clear();
        sfxSources.Clear();

        FindAudioSourcesByTag("BGM", ref bgmSources);
        FindAudioSourcesByTag("SFX", ref sfxSources);
    }

    private void ApplySavedVolumes()
    {
        float masterVolume = PlayerPrefs.GetInt(VolumeKey + "Master", 10) / 10f;
        float bgmVolume = PlayerPrefs.GetInt(VolumeKey + "BGM", 10) / 10f;
        float sfxVolume = PlayerPrefs.GetInt(VolumeKey + "SFX", 10) / 10f;

        Debug.Log($"[DEBUG] Applying Volumes: Master({masterVolume}) BGM({bgmVolume}) SFX({sfxVolume})");

        SetVolumeByTag("Master", masterVolume);
        SetVolumeByTag("BGM", bgmVolume * masterVolume);
        SetVolumeByTag("SFX", sfxVolume * masterVolume);
    }

    private void FindAudioSourcesByTag(string tag, ref List<AudioSource> audioSourcesList)
    {
        GameObject audioContainer = GameObject.FindGameObjectWithTag(tag);
        if (audioContainer != null)
        {
            audioSourcesList.AddRange(audioContainer.GetComponentsInChildren<AudioSource>());
        }
        else
        {
            Debug.LogWarning($"[SFXManager] No GameObject found with tag '{tag}'");
        }
    }

    public void SetVolumeByTag(string tag, float volume)
    {
        float adjustedVolume = Mathf.Max(volume, 0.0001f);

        if (audioMixer != null)
        {
            float dbValue = Mathf.Log10(adjustedVolume) * 20;
            audioMixer.SetFloat(tag, dbValue);
        }

        if (tag == "Master")
        {
            if (volume > 0)
            {
                lastMasterVolume = volume;
                AudioListener.pause = false;
            }

            AudioListener.volume = adjustedVolume;
        }
        else if (tag == "BGM")
        {
            foreach (var source in bgmSources)
            {
                if (source != null) source.volume = adjustedVolume;
            }
        }
        else if (tag == "SFX")
        {
            foreach (var source in sfxSources)
            {
                if (source != null) source.volume = adjustedVolume;
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSources.Count == 0) return;

        float sfxVolume = Mathf.Max(PlayerPrefs.GetInt(VolumeKey + "SFX", 10) / 10f, 0.0001f);
        float masterVolume = Mathf.Max(PlayerPrefs.GetInt(VolumeKey + "Master", 10) / 10f, 0.0001f);
        float finalVolume = sfxVolume * masterVolume;

        foreach (var source in sfxSources)
        {
            if (source != null && !source.isPlaying)
            {
                source.volume = finalVolume;
                source.PlayOneShot(clip);
                return;
            }
        }

        if (sfxSources[0] != null)
        {
            sfxSources[0].volume = finalVolume;
            sfxSources[0].PlayOneShot(clip);
        }
    }

    public AudioSource GetSFXSource()
    {
        return sfxSources.Count > 0 ? sfxSources[0] : null;
    }
}
