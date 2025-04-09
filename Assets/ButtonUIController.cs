using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUIController : MonoBehaviour
{
    public Button[] menuButtons;
    public AudioSource sfxAudioSource;
    public AudioClip buttonClickSound;

    private void Start()
    {
        gameObject.SetActive(false);
        AssignAudioSource();
        AddButtonListeners();
    }

    private void OnEnable()
    {
        if (!PauseMenu.GameIsPaused)
            gameObject.SetActive(false);
    }

    private void AssignAudioSource()
    {
        if (SFXManager.instance != null)
            sfxAudioSource = SFXManager.instance.GetSFXSource();
    }

    private void AddButtonListeners()
    {
        foreach (Button button in menuButtons)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    private void PlayClickSound()
    {
        if (sfxAudioSource != null && buttonClickSound != null)
            sfxAudioSource.PlayOneShot(buttonClickSound);
    }
}
