using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVolumeController : MonoBehaviour
{
    //[SerializeField] private AudioMixer myMixer;
    //[SerializeField] private Slider MasterSlider;
    //[SerializeField] private Slider MusicSlider;
    //[SerializeField] private Slider SFXSlider;

    //private void Start()
    //{
    //    // Load saved values or default to 1
    //    MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
    //    MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    //    SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

    //    ApplyVolumes();

    //    // Add listeners to sliders
    //    MasterSlider.onValueChanged.AddListener(delegate { SetMasterVolume(); });
    //    MusicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
    //    SFXSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
    //}

    //public void SetMasterVolume()
    //{
    //    float volume = MasterSlider.value;
    //    myMixer.SetFloat("Master", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    //    PlayerPrefs.SetFloat("MasterVolume", volume);
    //    PlayerPrefs.Save();
    //}

    //public void SetMusicVolume()
    //{
    //    float volume = MusicSlider.value;
    //    myMixer.SetFloat("Music", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    //    PlayerPrefs.SetFloat("MusicVolume", volume);
    //    PlayerPrefs.Save();
    //}

    //public void SetSFXVolume()
    //{
    //    float volume = SFXSlider.value;
    //    myMixer.SetFloat("SFX", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    //    PlayerPrefs.SetFloat("SFXVolume", volume);
    //    PlayerPrefs.Save();
    //}

    //private void ApplyVolumes()
    //{
    //    SetMasterVolume();
    //    SetMusicVolume();
    //    SetSFXVolume();
    //}
}
