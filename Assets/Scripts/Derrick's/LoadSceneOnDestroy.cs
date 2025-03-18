using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnDestroy : MonoBehaviour
{
    public string nextSceneName = "CreditsScreen"; // Set this to your actual scene name

    void OnDestroy()
    {
        // Check if the scene is not unloading (to avoid errors)
        if (gameObject.scene.isLoaded)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
