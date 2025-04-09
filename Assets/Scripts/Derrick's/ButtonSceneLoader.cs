using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSceneLoader : MonoBehaviour
{
    
    public Transform player; 
    public Vector3 spawnPosition;
    public void LoadScene(string sceneName)
    {
        if (player == CompareTag("Player"))
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

            //Time.timeScale = 1;
            //AudioListener.pause = false;

            SceneManager.LoadScene(2);
        }
    }
}