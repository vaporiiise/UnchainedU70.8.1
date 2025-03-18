using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetSelectionOnMouseMove : MonoBehaviour
{
    private Vector3 lastMousePosition;
    private bool mouseMoved;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        // Check if mouse position has changed
        if (Input.mousePosition != lastMousePosition)
        {
            mouseMoved = true;
            lastMousePosition = Input.mousePosition;

            // Reset keyboard selection if mouse moved
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            mouseMoved = false;
        }
    }
}