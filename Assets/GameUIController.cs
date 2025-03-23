using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameUIController : MonoBehaviour
{
    public Button[] menuButtons; // Assign UI Buttons in Inspector
    public AudioSource sfxAudioSource; // Assign an AudioSource for sound effects
    public AudioClip buttonClickSound;
    public AudioClip buttonHoverSound;

    private int selectedIndex = 0;

    private void Start()
    {
        gameObject.SetActive(false); // Disable at start
        AssignAudioSource();
        AddButtonListeners();
    }

    private void Update()
    {
        if (!PauseMenu.GameIsPaused) return;

        // Move Down (↓ or S)
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1) % menuButtons.Length;
            PlayHoverSound();
            HighlightButton();
        }

        // Move Up (↑ or W)
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex = (selectedIndex - 1 + menuButtons.Length) % menuButtons.Length;
            PlayHoverSound();
            HighlightButton();
        }

        // Select (Enter or Space)
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            PlayClickSound();
            menuButtons[selectedIndex].onClick.Invoke();
        }
    }

    private void OnEnable()
    {
        if (PauseMenu.GameIsPaused) HighlightButton();
    }

    private void HighlightButton()
    {
        EventSystem.current.SetSelectedGameObject(menuButtons[selectedIndex].gameObject);
    }

    private void PlayClickSound()
    {
        if (sfxAudioSource != null && buttonClickSound != null)
        {
            sfxAudioSource.PlayOneShot(buttonClickSound);
        }
    }

    private void PlayHoverSound()
    {
        if (sfxAudioSource != null && buttonHoverSound != null)
        {
            sfxAudioSource.PlayOneShot(buttonHoverSound);
        }
    }

    private void AssignAudioSource()
    {
        if (SFXManager.instance != null)
        {
            sfxAudioSource = SFXManager.instance.GetSFXSource();
        }
    }

    private void AddButtonListeners()
    {
        foreach (Button button in menuButtons)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }
}
