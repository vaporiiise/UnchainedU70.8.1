using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; } = false;

    public static void PauseGame()
    {
        IsGamePaused = true;
    }

    public static void ResumeGame()
    {
        IsGamePaused = false;
    }
}