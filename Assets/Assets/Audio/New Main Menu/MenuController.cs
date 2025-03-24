using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public Button[] menuButtons; // Assign UI Buttons in Inspector
    public AudioSource sfxAudioSource; // Assign an AudioSource for sound effects
    public AudioClip buttonClickSound; // Assign a sound clip for button clicks
    public AudioClip buttonHoverSound; // Assign a sound clip for button hover

    private int selectedIndex = 0;

    private void Start()
    {
        HighlightButton();

        if (SFXManager.instance != null && SFXManager.instance.GetSFXSource() != null)
        {
            sfxAudioSource = SFXManager.instance.GetSFXSource();
        }
        else
        {
            Debug.LogError("MenuController: No SFX AudioSource found in SFXManager!");
        }

        foreach (Button button in menuButtons)
        {
            button.onClick.AddListener(() => PlayClickSound());
        }

        DebugMixerGroup();

        //HighlightButton();

        //// Add a click sound effect to each button
        //foreach (Button button in menuButtons)
        //{
        //    button.onClick.AddListener(() => PlayClickSound());
        //}

        //// Debug Mixer Assignment
        //DebugMixerGroup(); //added by Jason for debugAudioMixer
    }

    private void Update()
    {
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

    private void HighlightButton()
    {
        // Highlight the selected button
        EventSystem.current.SetSelectedGameObject(menuButtons[selectedIndex].gameObject);
    }

    private void PlayClickSound() //Jason's ver
    {
        if (sfxAudioSource == null)
        {
            Debug.LogError(" No AudioSource assigned for SFX!");
            return;
        }

        if (buttonClickSound == null)
        {
            Debug.LogError(" No button click sound assigned!");
            return;
        }

        //Debug.Log(" Playing Click Sound...");
        sfxAudioSource.PlayOneShot(buttonClickSound);

        //private void PlayClickSound()
        //{
        //    if (audioSource != null && buttonClickSound != null)
        //    {
        //        audioSource.PlayOneShot(buttonClickSound);
        //    }
        //}
    }



    private void PlayHoverSound() //Jason's ver
    {
        if (sfxAudioSource == null)
        {
            Debug.LogError(" No AudioSource assigned for SFX!");
            return;
        }

        if (buttonHoverSound == null)
        {
            Debug.LogError(" No hover sound assigned!");
            return;
        }

        Debug.Log("Playing Hover Sound...");
        sfxAudioSource.PlayOneShot(buttonHoverSound);

        //private void PlayHoverSound()
        //{
        //    if (audioSource != null && buttonHoverSound != null)
        //    {
        //        audioSource.PlayOneShot(buttonHoverSound);
        //    }
        //}
    }

    //Added by Jason for debuggingAudioMix making sure everything plays as it should
    private void DebugMixerGroup()
    {
        if (sfxAudioSource != null)
        {
            if (sfxAudioSource.outputAudioMixerGroup == null)
            {
                Debug.LogError(" SFX AudioSource is NOT assigned to an Audio Mixer Group!");
            }
            else
            {
                Debug.Log(" SFX AudioSource is correctly assigned to: " + sfxAudioSource.outputAudioMixerGroup.name);
            }
        }
        else
        {
            Debug.LogError(" SFX AudioSource is missing!");
        }
    }
}