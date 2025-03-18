using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    void Awake()
    {
        // Check if there's already an instance of PlayerManager
        if (instance == null)
        {
            // If no instance exists, set this as the instance and mark it to persist
            instance = this;
            DontDestroyOnLoad(gameObject);  // Prevent destruction on scene load
        }
        else
        {
            // If an instance already exists (i.e., after a scene reset), destroy the new one
            Destroy(gameObject);
        }
    }
}
