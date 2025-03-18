using UnityEngine;

public class CanvasSwaper : MonoBehaviour
{
    public Canvas currentCanvas;
    public Canvas targetCanvas;
    private CanvasFader canvasFader;

    void Start()
    {
        if (targetCanvas == null)
        {
            Debug.LogWarning("[CanvasSwaper] No target canvas assigned on " + gameObject.name);
        }

        // Try to find a CanvasFader component on this button
        canvasFader = GetComponent<CanvasFader>();
    }

    public void SwapCanvas()
    {
        if (canvasFader != null)
        {
            // If there's a fader, let it handle the swap with fade
            canvasFader.FadeAndSwap();
        }
        else
        {
            // Instant swap if no fader
            if (currentCanvas != null) currentCanvas.gameObject.SetActive(false);
            if (targetCanvas != null) targetCanvas.gameObject.SetActive(true);
        }
    }
}
