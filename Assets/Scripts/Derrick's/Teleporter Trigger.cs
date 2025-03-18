using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTrigger : MonoBehaviour
{
    public Vector2 teleportPosition; // Set this in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the "Player" tag
        {
            other.transform.position = teleportPosition;
            Debug.Log("Player teleported to: " + teleportPosition);
        }
    }
}
