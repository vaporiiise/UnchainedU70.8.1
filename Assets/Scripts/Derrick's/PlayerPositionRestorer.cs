using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionRestorer : MonoBehaviour
{
    private void Start()
    {
        // Check if there is a saved player position in the GameManager
        if (GameManager.Instance != null && GameManager.Instance.savedPlayerPosition != Vector3.zero)
        {
            // Set the player's position to the saved position
            transform.position = GameManager.Instance.savedPlayerPosition;
            Debug.Log("Player position restored to: " + transform.position);
        }
    }
}
