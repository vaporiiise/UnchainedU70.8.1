using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthGameManager : MonoBehaviour
{
    public static BossHealthGameManager Instance;

    public int savedHealth = -1; // -1 means no saved health yet

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}