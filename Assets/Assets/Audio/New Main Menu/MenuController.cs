using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MenuController : MonoBehaviour
{
    public Button[] menuButtons; // Assign UI Buttons in Inspector
    public AudioSource audioSource; // Assign an AudioSource in Inspector
    public AudioClip buttonClickSound; // Assign a sound clip for button clicks
    public AudioClip buttonHoverSound; // Assign a sound clip for button hover

    private int selectedIndex = 0;

    private void Start()
    {
        HighlightButton();

        // Add a click sound effect to each button
        foreach (Button button in menuButtons)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    private void Update()
    {
        // Move Down (↓ or S)
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1) % menuButtons.Length;
            PlayHoverSound(); // Play hover sound effect
            HighlightButton();
        }

        // Move Up (↑ or W)
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex = (selectedIndex - 1 + menuButtons.Length) % menuButtons.Length;
            PlayHoverSound(); // Play hover sound effect
            HighlightButton();
        }

        // Select (Enter or Space)
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            PlayClickSound(); // Play click sound effect
            menuButtons[selectedIndex].onClick.Invoke(); // Trigger the selected button
        }
    }

    private void HighlightButton()
    {
        // Highlight the selected button
        EventSystem.current.SetSelectedGameObject(menuButtons[selectedIndex].gameObject);
    }

    private void PlayClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    private void PlayHoverSound()
    {
        if (audioSource != null && buttonHoverSound != null)
        {
            audioSource.PlayOneShot(buttonHoverSound);
        }
    }
}