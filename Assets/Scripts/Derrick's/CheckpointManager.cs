using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    private Vector2 currentCheckpoint;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Set the current checkpoint position
    public void SetCurrentCheckpoint(Vector2 checkpointPosition)
    {
        currentCheckpoint = checkpointPosition;
        Debug.Log("Checkpoint set at position: " + currentCheckpoint);
    }

    // Get the position of the current checkpoint
    public Vector2 GetCurrentCheckpoint()
    {
        return currentCheckpoint;
    }
}
