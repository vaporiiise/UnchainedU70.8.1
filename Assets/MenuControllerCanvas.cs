using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControllerCanvas : MonoBehaviour
{
    public Canvas[] canvases; // Assign in Inspector, Main Menu should be first
    private int currentCanvasIndex = 0;

    void Start()
    {
        // Ensure only the main menu (first canvas) is active at the start
        for (int i = 1; i < canvases.Length; i++)
        {
            canvases[i].gameObject.SetActive(false);
        }
    }

    public void ShowCanvas(int index)
    {
        if (index < 0 || index >= canvases.Length) return;

        // Hide the currently active canvas
        canvases[currentCanvasIndex].gameObject.SetActive(false);

        // Show the new canvas
        canvases[index].gameObject.SetActive(true);

        // Update current canvas index
        currentCanvasIndex = index;
    }

    public void BackToMainMenu()
    {
        ShowCanvas(0); // Always go back to the main menu
    }
}
