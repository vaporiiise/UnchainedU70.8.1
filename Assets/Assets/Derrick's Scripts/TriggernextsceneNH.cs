using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggernextsceneNH : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public int nextScene; // Name of the next scene to load
    public Vector2 areaMin; // Bottom-left corner of the area
    public Vector2 areaMax; // Top-right corner of the area
    
    

    void Update()
    {
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;
            if (playerPosition.x >= areaMin.x && playerPosition.x <= areaMax.x &&
                playerPosition.y >= areaMin.y && playerPosition.y <= areaMax.y)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    LoadNextScene();
                }
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
        
    }

    // Draw the area in the Scene view for visualization
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 bottomLeft = new Vector3(areaMin.x, areaMin.y, 0f);
        Vector3 topRight = new Vector3(areaMax.x, areaMax.y, 0f);
        Vector3 topLeft = new Vector3(areaMin.x, areaMax.y, 0f);
        Vector3 bottomRight = new Vector3(areaMax.x, areaMin.y, 0f);

        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }
}
