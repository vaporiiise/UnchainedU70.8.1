using System.Collections;
using UnityEngine;

public class CanvasFader : MonoBehaviour
{
    public Canvas currentCanvas;
    public Canvas targetCanvas;
    private CanvasGroup currentGroup;
    private CanvasGroup targetGroup;

    private int frameRate = 10;
    private bool isFading = false;

    void Start()
    {
        if (currentCanvas != null)
        {
            currentGroup = currentCanvas.GetComponent<CanvasGroup>() ?? currentCanvas.gameObject.AddComponent<CanvasGroup>();
            ResetCanvasGroup(currentGroup, true);
        }

        if (targetCanvas != null)
        {
            targetGroup = targetCanvas.GetComponent<CanvasGroup>() ?? targetCanvas.gameObject.AddComponent<CanvasGroup>();
            ResetCanvasGroup(targetGroup, false);
            targetCanvas.gameObject.SetActive(false); // Ensure it's disabled initially
        }
    }

    public void FadeAndSwap()
    {
        if (isFading) return;
        if (currentCanvas == null || targetCanvas == null)
        {
            Debug.LogError("Canvas reference missing!");
            return;
        }

        StartCoroutine(FadeOutAndSwap());
    }

    private IEnumerator FadeOutAndSwap()
    {
        isFading = true;
        float frameTime = 1f / frameRate;
        float fadeStep = frameTime;

        Debug.Log($"[{gameObject.name}] Starting Fade Out...");

        // Fade Out Current Canvas
        for (float t = 1; t > 0; t -= fadeStep)
        {
            currentGroup.alpha = t;
            yield return new WaitForSeconds(frameTime);
        }

        currentGroup.alpha = 0;
        ResetCanvasGroup(currentGroup, false);
        currentCanvas.gameObject.SetActive(false);

        Debug.Log($"[{gameObject.name}] Fade Out Complete. Swapping Canvas...");

        // Activate and Reset Target Canvas
        targetCanvas.gameObject.SetActive(true);
        ResetCanvasGroup(targetGroup, true);

        Debug.Log($"[{gameObject.name}] Target Canvas Instantly Visible!");

        isFading = false;
    }

    private void ResetCanvasGroup(CanvasGroup group, bool isActive)
    {
        group.alpha = isActive ? 1 : 0;
        group.interactable = isActive;
        group.blocksRaycasts = isActive;
    }
}
