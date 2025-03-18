using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovementIDeath : MonoBehaviour
{
    public Canvas canvas; // Drag your canvas here in the Inspector
    private MonoBehaviour scriptToDisable;

    void Start()
    {
        scriptToDisable = GetComponent<MonoBehaviour>(); // Automatically gets the script attached to this GameObject
    }

    void Update()
    {
        if (canvas.isActiveAndEnabled) // Check if the canvas is active
        {
            scriptToDisable.enabled = false; // Disable the script
        }
        else
        {
            scriptToDisable.enabled = true; // Enable the script
        }
    }
}
