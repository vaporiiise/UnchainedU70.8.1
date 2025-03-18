using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName; // Specify the name of the scene in the Inspector

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
