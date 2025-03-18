using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused { get; private set; }

    public GameObject pauseMenuUI; // Ref to the pause menu UI
    public GameObject settingsPanel; // Ref to the settings panel

    public GameObject player; // Ref to the player GameObject

    [SerializeField]
    private KeyCode pauseKey = KeyCode.Escape; // Configurable key for pausing the game

    private MonoBehaviour[] playerScripts; // Store the player scripts for enabling/disabling

    void Start()
    {
        // Ensure the settings panel is hidden at the start, if assigned
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        // Ensure the pause menu is hidden at the start, if assigned
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        // Get all the MonoBehaviour scripts attached to the player, if assigned
        if (player != null)
        {
            playerScripts = player.GetComponents<MonoBehaviour>();
        }
    }

    void Update()
    {
        // Check if the pause key is pressed, and the pauseMenuUI exists
        if (pauseMenuUI != null && Input.GetKeyDown(pauseKey))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public static void TogglePause()
    {
        GameIsPaused = !GameIsPaused;

        // Optionally manage the game time scale
        Time.timeScale = GameIsPaused ? 0f : 1f;
    }

    void Resume()
    {
        // Only resume if the pause menu UI exists
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false); // Hide the pause menu UI
        }

        // Always hide the settings panel, if exists
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false); // Ensure settings panel is hidden
        }

        Time.timeScale = 1f; // Resume the game time
        GameIsPaused = false; // Update the game state

        // Re-enable player scripts if player exists
        EnablePlayerScripts();
    }

    void Pause()
    {
        // Only pause if the pause menu UI exists
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true); // Show the pause menu UI
        }

        Time.timeScale = 0f; // Pause the game time
        GameIsPaused = true; // Update the game state

        // Disable player scripts if player exists
        DisablePlayerScripts();
    }

    // Method to open the settings panel
    public void OpenSettingsPanel()
    {
        // Hide the pause menu UI, if exists
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        // Show the settings panel if it exists
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    // Method to close the settings panel and return to the pause menu (if it exists)
    public void CloseSettingsPanel()
    {
        // Hide the settings panel if it exists
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        // Show the pause menu UI again if it exists
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
    }

    // Method to disable all player scripts
    void DisablePlayerScripts()
    {
        if (playerScripts != null)
        {
            foreach (MonoBehaviour script in playerScripts)
            {
                if (script != this) // Ensure we don't disable the PauseMenu itself
                {
                    script.enabled = false;
                }
            }
        }
    }

    // Method to enable all player scripts
    void EnablePlayerScripts()
    {
        if (playerScripts != null)
        {
            foreach (MonoBehaviour script in playerScripts)
            {
                script.enabled = true;
            }
        }
    }
}
