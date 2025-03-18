using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSceneLoader : MonoBehaviour
{
    public GameObject player;  // Reference to the player GameObject
    public int sceneName = 1;   // The name of the scene to load

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
