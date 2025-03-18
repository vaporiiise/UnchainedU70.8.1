using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettingsPanel : MonoBehaviour
{
    public GameObject settingsPanel; // The settings panel UI
    public Slider volumeSlider; // Background music volume slider
    public AudioSource backgroundMusic; // Background music source

    public Slider sfxSlider; // SFX volume slider
    public AudioSource[] soundEffects; // Array of SFX AudioSources

    private const string VolumeKey = "BackgroundVolume"; // Save key for background volume
    private const string SfxVolumeKey = "SFXVolume"; // Save key for SFX volume

    private void Start()
    {
        // Load saved volume settings
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        float savedSfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey, 1f);

        // Apply background music volume
        if (backgroundMusic != null && volumeSlider != null)
        {
            backgroundMusic.volume = savedVolume;
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetBackgroundVolume);
        }

        // Apply SFX volume
        if (soundEffects != null && sfxSlider != null)
        {
            sfxSlider.value = savedSfxVolume;
            sfxSlider.onValueChanged.AddListener(SetSfxVolume);
            SetSfxVolume(savedSfxVolume);
        }
    }

    // Open the settings panel
    public void OpenSettingsPanel()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    // Close the settings panel
    public void CloseSettingsPanel()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    public void SetBackgroundVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume;
            PlayerPrefs.SetFloat(VolumeKey, volume);
            PlayerPrefs.Save();
        }
    }

    public void SetSfxVolume(float volume)
    {
        PlayerPrefs.SetFloat(SfxVolumeKey, volume);
        PlayerPrefs.Save();

        if (soundEffects != null)
        {
            foreach (AudioSource sfx in soundEffects)
            {
                if (sfx != null)
                {
                    sfx.volume = volume;
                }
            }
        }
    }
}
