    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSCControl : MonoBehaviour
{
    public Transform player; 
    public Vector3 spawnPosition; 
    public PauseMenu pauseMenu;

    public void RestartGame()
    {
        if (player != null)
        {
            player.position = spawnPosition;
        }
        else
        {
            Debug.LogWarning("Player not assigned.");
        }
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }

        Time.timeScale = 1; 
        AudioListener.pause = false;
        pauseMenu.Resume();

        SceneManager.LoadScene(2); 
    }

    public void RestartBossFight()
    {
        Time.timeScale = 1; 
        AudioListener.pause = false;

        SceneManager.LoadScene(3); 

    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
