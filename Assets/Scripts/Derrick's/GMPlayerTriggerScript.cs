using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMPlayerTriggerScript : MonoBehaviour
{
    public Transform player; // Reference to the player

    public Vector2 minPosition; // Minimum x, y position for the area
    public Vector2 maxPosition; // Maximum x, y position for the area

    public MonoBehaviour[] scriptsToToggle;
    public MonoBehaviour[] scriptsToOffToggle;// Array of scripts to enable/disable

    private bool isPlayerInside = false; // Check if the player is inside the area

    void Update()
    {
        // Check if the player is inside the defined area
        if (!isPlayerInside && player.position.x >= minPosition.x && player.position.x <= maxPosition.x &&
            player.position.y >= minPosition.y && player.position.y <= maxPosition.y)
        {
            EnterArea();
        }
        else if (isPlayerInside && (player.position.x < minPosition.x || player.position.x > maxPosition.x ||
            player.position.y < minPosition.y || player.position.y > maxPosition.y))
        {
            ExitArea();
        }
    }

    // Enable the scripts when the player enters the area
    void EnterArea()
    {
        isPlayerInside = true;
        foreach (MonoBehaviour script in scriptsToToggle)
        {
            script.enabled = true; // Enable the script
        }
        isPlayerInside = true;
        foreach (MonoBehaviour script in scriptsToOffToggle)
        {
            script.enabled = false; 
        }
    }

    // Disable the scripts when the player leaves the area
    void ExitArea()
    {
        isPlayerInside = false;
        foreach (MonoBehaviour script in scriptsToToggle)
        {
            script.enabled = false; // Disable the script
        }
        isPlayerInside = false;
        foreach (MonoBehaviour script in scriptsToOffToggle)
        {
            script.enabled = true; // Disable the script
        }
    }

    // Draw Gizmos to visualize the min/max area in the Scene view
    private void OnDrawGizmos()
    {
        // Set the color for the Gizmos
        Gizmos.color = Color.red;

        // Draw a wireframe box representing the area
        Vector2 size = new Vector2(maxPosition.x - minPosition.x, maxPosition.y - minPosition.y);
        Vector3 center = new Vector3((minPosition.x + maxPosition.x) / 2, (minPosition.y + maxPosition.y) / 2, 0);

        // Draw a wire cube in the scene view representing the area
        Gizmos.DrawWireCube(center, size);
    }
}