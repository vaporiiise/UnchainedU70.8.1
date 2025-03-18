using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalResetManager : MonoBehaviour
{
    public static GlobalResetManager Instance;

    private List<IResettable> resettables = new List<IResettable>();

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

    // Register a resettable object
    public void RegisterResettable(IResettable resettable)
    {
        if (!resettables.Contains(resettable))
        {
            resettables.Add(resettable);
        }
    }

    // Reset all registered resettables to their initial state
    public void ResetAllExceptPlayer()
    {
        foreach (var resettable in resettables)
        {
            resettable.ResetState();
        }

        Debug.Log("All game objects have been reset, except the player position.");
    }
}
