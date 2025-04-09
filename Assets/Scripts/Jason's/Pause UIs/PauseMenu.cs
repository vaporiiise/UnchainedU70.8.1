using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused { get; private set; }

    public GameObject pauseMenuUI;
    public GameObject settingsPanel;
    public GameObject exitPanel;
    public GameObject player;

    public ButtonUIController buttonUIController;

    [SerializeField]
    private KeyCode pauseKey = KeyCode.Escape;

    private MonoBehaviour[] playerScripts;

    [Header("Dialogue Systems to Pause")]
    [SerializeField] private List<DialogueSystem> dialogueSystems = new List<DialogueSystem>();

    private List<DialogueSystem> pausedDialogues = new List<DialogueSystem>();
    void Start()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);

        if (player != null)
        {
            playerScripts = player.GetComponents<MonoBehaviour>();
        }

        if (buttonUIController != null)
        {
            buttonUIController.enabled = false;
        }
    }

    void Update()
    {
        if (pauseMenuUI != null && Input.GetKeyDown(pauseKey))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public static void TogglePause()
    {
        GameIsPaused = !GameIsPaused;
        Time.timeScale = GameIsPaused ? 0f : 1f;
    }

    public void Resume()
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);

        Time.timeScale = 1f;
        GameIsPaused = false;
        EnablePlayerScripts();

        if (buttonUIController != null)
        {
            buttonUIController.enabled = false;
        }

        ResumePausedDialogues();
    }

    void Pause()
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;
        DisablePlayerScripts();

        if (buttonUIController != null)
        {
            buttonUIController.enabled = true;
        }

        PauseActiveDialogues();
    }

    void PauseActiveDialogues()
    {
        pausedDialogues.Clear();
        foreach (DialogueSystem dialogue in dialogueSystems)
        {
            if (dialogue.IsDialogueActive())
            {
                dialogue.enabled = false;
                pausedDialogues.Add(dialogue);
            }
        }
    }

    void ResumePausedDialogues()
    {
        foreach (DialogueSystem dialogue in pausedDialogues)
        {
            dialogue.enabled = true;
        }
        pausedDialogues.Clear();
    }

    public void OpenSettingsPanel()
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void CloseSettingsPanel()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
    }

    public void OpenExitPanel()
    {
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (exitPanel != null) exitPanel.SetActive(true);
    }

    public void CloseExitPanel()
    {
        if (exitPanel != null) exitPanel.SetActive(false);
        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
    }

    public void ConfirmExit()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    void DisablePlayerScripts()
    {
        if (playerScripts != null)
        {
            foreach (MonoBehaviour script in playerScripts)
            {
                if (script != this)
                {
                    script.enabled = false;
                }
            }
        }
    }

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
