using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Import DOTween


public class ShakeHealthBar : MonoBehaviour
{
    public RectTransform healthBar; // Assign the Health Bar's RectTransform in Inspector

    [Header("Shake Settings")]
    public float shakeStrength = 5f;  // How far it shakes
    public float shakeDuration = 0.2f; // How long it shakes
    public int shakeVibrato = 10; // Number of shakes
    public bool fadeOut = true; // If shake fades out

    public void HealthBarShake()
    {
        if (healthBar == null)
        {
            Debug.LogError("[ShakeHealthBar] Health bar RectTransform is not assigned!");
            return;
        }

        Debug.Log("[ShakeHealthBar] Shaking Health Bar..."); // Debug Log

        // Shake using DOAnchorPos for UI elements
        healthBar.DOShakeAnchorPos(shakeDuration, shakeStrength, shakeVibrato, 90, false, fadeOut);
    }

}