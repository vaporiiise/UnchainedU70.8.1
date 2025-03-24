using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelToggle : MonoBehaviour
{
    public GameObject targetPanel; // Assign the confirm exit panel in the Inspector

    void Start()
    {
        if (targetPanel != null)
        {
            targetPanel.SetActive(false); // Ensure the panel starts hidden
        }
    }

    public void ShowPanel()
    {
        if (targetPanel != null)
        {
            targetPanel.SetActive(true);
        }
    }

    public void HidePanel()
    {
        if (targetPanel != null)
        {
            targetPanel.SetActive(false);
        }
    }

    public void SwitchScene(string sceneName)
    {
        Debug.Log("Switching to scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

}
