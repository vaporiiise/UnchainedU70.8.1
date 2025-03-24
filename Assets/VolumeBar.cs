using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeBar : MonoBehaviour
{
    public string audioTag = "BGM"; // "Master", "BGM", or "SFX"
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

        float newVolume = volumeLevel / (float)volumeBars.Length;
        PlayerPrefs.SetInt(VolumeKey + audioTag, volumeLevel);
        PlayerPrefs.Save();

        Debug.Log($"[DEBUG] {audioTag} Volume Updated: {newVolume}");

        if (audioTag == "Master")
        {
            int bgmStored = PlayerPrefs.GetInt(VolumeKey + "BGM", volumeBars.Length);
            int sfxStored = PlayerPrefs.GetInt(VolumeKey + "SFX", volumeBars.Length);

            float bgmVolume = bgmStored / (float)volumeBars.Length;
            float sfxVolume = sfxStored / (float)volumeBars.Length;

            float finalBGM = (bgmStored == 0) ? 0f : bgmVolume * newVolume;
            float finalSFX = (sfxStored == 0) ? 0f : sfxVolume * newVolume;

            Debug.Log($"[DEBUG] Applying Master Volume Scaling: Master({newVolume}) → BGM({finalBGM}) SFX({finalSFX})");

            SFXManager.instance.SetVolumeByTag("BGM", finalBGM);
            SFXManager.instance.SetVolumeByTag("SFX", finalSFX);
            SFXManager.instance.SetVolumeByTag("Master", newVolume); // Ensure Master is also updated
        }
        else
        {
            float masterVolume = PlayerPrefs.GetInt(VolumeKey + "Master", volumeBars.Length) / (float)volumeBars.Length;
            newVolume = Mathf.Min(newVolume, masterVolume);

            Debug.Log($"[DEBUG] Applying {audioTag} Volume: {newVolume}");
            SFXManager.instance.SetVolumeByTag(audioTag, newVolume);
        }
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
