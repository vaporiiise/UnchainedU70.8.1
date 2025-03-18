using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector3 savedPlayerPosition;
    public int savedPlayerHealth;  // Variable to store saved player health
    public GameObject objectToDisable; // GameObject to disable when health reaches 10

    private void Awake()
    {
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

    public void SavePlayerPosition(Vector3 position)
    {
        savedPlayerPosition = position;
        Debug.Log("Player position saved: " + savedPlayerPosition);
    }

    public void SavePlayerHealth(int health)
    {
        savedPlayerHealth = health;
        Debug.Log("Player health saved: " + savedPlayerHealth);

        if (savedPlayerHealth <= 10 && objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
    }
}
