    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSCControl : MonoBehaviour
{
    public Transform player; // Assign your player here
    public Vector3 spawnPosition; // The position to send the player to

    public void RestartGame()
    {
        // Move player to the specified spawn position before restarting
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

        // Reset game time scale and audio settings
        Time.timeScale = 1; 
        AudioListener.pause = false;

        // Restart the current scene
        SceneManager.LoadScene(1); 
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
