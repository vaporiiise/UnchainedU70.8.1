using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AbortButton : MonoBehaviour
{
    public string sceneToLoad;
    public Button abortButton;

    void Start()
    {
        if (abortButton != null)
        {
            abortButton.onClick.AddListener(ChangeScene);
        }
        else
        {
            Debug.LogError("AbortButton is not assigned in " + gameObject.name);
        }
    }

    void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Loading scene: " + sceneToLoad);
            PauseMenu.TogglePause();
            SceneManager.LoadScene(sceneToLoad);

        }
        else
        {
            Debug.LogError("Scene name is not set in the Inspector!");
        }
    }
}
