using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject mainCanvas;  // The canvas that triggers the change
    public List<GameObject> otherCanvases; // Canvases to disable

    void Update()
    {
        if (mainCanvas.activeSelf) // If mainCanvas is active
        {
            DisableOtherCanvases();
        }
    }

    void DisableOtherCanvases()
    {
        foreach (GameObject canvas in otherCanvases)
        {
            if (canvas != null && canvas.activeSelf)
            {
                canvas.SetActive(false);
            }
        }
    }
}
