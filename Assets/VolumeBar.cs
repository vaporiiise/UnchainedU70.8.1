using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeBar : MonoBehaviour
{
    public string audioTag = "BGM"; // "Master" or "BGM" or "SFX"
    public Image[] volumeBars;
    public Button increaseButton, decreaseButton;

    private int volumeLevel;
    private const string VolumeKey = "Volume_";

    private void Start()
    {
        volumeLevel = PlayerPrefs.GetInt(VolumeKey + audioTag, volumeBars.Length);
        volumeLevel = Mathf.Clamp(volumeLevel, 0, volumeBars.Length);

        UpdateVolumeBar();

        increaseButton.onClick.AddListener(IncreaseVolume);
        decreaseButton.onClick.AddListener(DecreaseVolume);
    }

    void IncreaseVolume()
    {
        if (volumeLevel < volumeBars.Length)
        {
            volumeLevel++;
            UpdateVolumeBar();
        }
    }

    void DecreaseVolume()
    {
        if (volumeLevel > 0)
        {
            volumeLevel--;
            UpdateVolumeBar();
        }
    }

    void UpdateVolumeBar()
    {
        for (int i = 0; i < volumeBars.Length; i++)
        {
            volumeBars[i].enabled = (i < volumeLevel);
        }

        // Convert bar count to 0-1 range
        float newVolume = volumeLevel / (float)volumeBars.Length;

        if (audioTag == "Master")
        {
            AudioManager.instance.SetVolumeByTag("BGM", newVolume);
            AudioManager.instance.SetVolumeByTag("SFX", newVolume);

            int bgmLevel = PlayerPrefs.GetInt(VolumeKey + "BGM", volumeBars.Length);
            int sfxLevel = PlayerPrefs.GetInt(VolumeKey + "SFX", volumeBars.Length);

            bgmLevel = Mathf.Min(bgmLevel, volumeLevel);
            sfxLevel = Mathf.Min(sfxLevel, volumeLevel);

            PlayerPrefs.SetInt(VolumeKey + "BGM", bgmLevel);
            PlayerPrefs.SetInt(VolumeKey + "SFX", sfxLevel);
        }
        else
        {
            // If changing BGM or SFX, ensure it does not exceed Master volume
            int masterLevel = PlayerPrefs.GetInt(VolumeKey + "Master", volumeBars.Length);
            volumeLevel = Mathf.Min(volumeLevel, masterLevel);

            AudioManager.instance.SetVolumeByTag(audioTag, newVolume);
        }

        // Save volume setting
        PlayerPrefs.SetInt(VolumeKey + audioTag, volumeLevel);
        PlayerPrefs.Save();
    }

    //public string audioTag = "BGM"; // "Master" or "BGM" or "SFX"
    //public Image[] volumeBars;
    //public Button increaseButton, decreaseButton;

    //private int volumeLevel;
    //private const string VolumeKey = "Volume_"; // Key for saving

    //private void Start()
    //{
    //    // Load volume setting from PlayerPrefs (default to full volume)
    //    volumeLevel = PlayerPrefs.GetInt(VolumeKey + audioTag, volumeBars.Length);
    //    volumeLevel = Mathf.Clamp(volumeLevel, 0, volumeBars.Length);

    //    UpdateVolumeBar();

    //    increaseButton.onClick.AddListener(IncreaseVolume);
    //    decreaseButton.onClick.AddListener(DecreaseVolume);
    //}

    //void IncreaseVolume()
    //{
    //    if (volumeLevel < volumeBars.Length)
    //    {
    //        volumeLevel++;
    //        UpdateVolumeBar();
    //    }
    //}

    //void DecreaseVolume()
    //{
    //    if (volumeLevel > 0)
    //    {
    //        volumeLevel--;
    //        UpdateVolumeBar();
    //    }
    //}

    //void UpdateVolumeBar()
    //{
    //    for (int i = 0; i < volumeBars.Length; i++)
    //    {
    //        volumeBars[i].enabled = (i < volumeLevel);
    //    }

    //    // Convert bar count to 0-1 range
    //    float newVolume = volumeLevel / (float)volumeBars.Length;
    //    AudioManager.instance.SetVolumeByTag(audioTag, newVolume);

    //    // Save volume setting
    //    PlayerPrefs.SetInt(VolumeKey + audioTag, volumeLevel);
    //    PlayerPrefs.Save();
    //}
}
