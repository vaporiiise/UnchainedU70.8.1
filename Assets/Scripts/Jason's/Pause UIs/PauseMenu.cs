using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused { get; private set; }

    public GameObject pauseMenuUI;  // Assign Pause Panel
    public GameObject settingsPanel; // Assign Settings Panel
    public GameObject exitPanel; // Assign Exit Panel

    [SerializeField]
    private KeyCode pauseKey = KeyCode.Escape;

    void Start()
    {
        // Ensure all panels are disabled at the start
        if (pauseMenuUI) pauseMenuUI.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (exitPanel) exitPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    public static void TogglePause()
    {
        PauseMenu instance = FindObjectOfType<PauseMenu>();
        if (instance != null)
        {
            if (GameIsPaused)
                instance.Resume();
            else
                instance.Pause();
        }
        else
        {
            Debug.LogError("PauseMenu instance not found in scene!");
        }
    }

    void Pause()
    {
        if (pauseMenuUI) pauseMenuUI.SetActive(true);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (exitPanel) exitPanel.SetActive(false);

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void Resume()
    {
        if (pauseMenuUI) pauseMenuUI.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
        if (exitPanel) exitPanel.SetActive(false);

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void OpenSettingsPanel()
    {
        if (settingsPanel) settingsPanel.SetActive(true);
        if (pauseMenuUI) pauseMenuUI.SetActive(false);
        if (exitPanel) exitPanel.SetActive(false);
    }

    public void CloseSettingsPanel()
    {
        if (settingsPanel) settingsPanel.SetActive(false);
        if (pauseMenuUI) pauseMenuUI.SetActive(true);
    }

    public void OpenExitPanel()
    {
        if (exitPanel) exitPanel.SetActive(true);
        if (pauseMenuUI) pauseMenuUI.SetActive(false);
    }

    public void CloseExitPanel()
    {
        if (exitPanel) exitPanel.SetActive(false);
        if (pauseMenuUI) pauseMenuUI.SetActive(true);
    }
}
