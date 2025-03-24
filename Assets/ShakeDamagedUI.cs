using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShakeDamagedUI : MonoBehaviour
{
    [Header("References")]
    public BHPlayerHeaalth playerHealth; // Drag and drop the Player Health script
    public RectTransform uiElement; // Drag the UI element to shake
    public Image uiImage; // Assign the UI image to switch
    public Sprite hurtSprite; // Hurt UI sprite (assign in inspector)

    private Sprite normalSprite; // Stores the default UI sprite
    private int lastHealth; // Tracks previous health

    [Header("Shake Settings")]
    public float shakeDuration = 0.2f;
    [Tooltip("Distance of shake")]
    public float shakeStrength = 10f;
    [Tooltip("Intensity of Shake")]
    public int shakeVibrato = 10;
    public bool fadeOut = true;
    public float hurtSpriteDuration = 0.2f; // How long to show hurt sprite

    void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogError("[ShakeDamagedUI] Player health script is not assigned!");
            return;
        }

        // Store initial health
        lastHealth = playerHealth.maxHealth;

        // Store the original UI sprite
        if (uiImage != null)
        {
            normalSprite = uiImage.sprite;
        }

        // Subscribe to health updates
        StartCoroutine(CheckForDamage());
    }

    IEnumerator CheckForDamage()
    {
        while (true)
        {
            if (playerHealth == null) yield break;

            int currentHealth = GetPlayerCurrentHealth();

            if (currentHealth < lastHealth)
            {
                ShakeUI(); // Shake and switch UI when damaged
            }

            lastHealth = currentHealth; // Update last health value
            yield return new WaitForSeconds(0.1f); // Small delay to avoid performance issues
        }
    }

    void ShakeUI()
    {
        if (uiElement == null || uiImage == null) return;

        Debug.Log("[ShakeDamagedUI] Shaking UI and changing to hurt sprite!");

        // Switch to hurt sprite if assigned
        if (hurtSprite != null)
        {
            uiImage.sprite = hurtSprite;
        }

        // Shake the UI element using DOTween
        uiElement.DOShakeAnchorPos(shakeDuration, shakeStrength, shakeVibrato, 90, false, fadeOut)
            .OnComplete(() =>
            {
                // Restore normal sprite after a short delay
                StartCoroutine(RestoreSpriteAfterDelay());
            });
    }

    IEnumerator RestoreSpriteAfterDelay()
    {
        yield return new WaitForSeconds(hurtSpriteDuration);

        if (uiImage != null && normalSprite != null)
        {
            uiImage.sprite = normalSprite;
        }
    }

    int GetPlayerCurrentHealth()
    {
        // Using reflection to get currentHealth since we can't modify BHPlayerHeaalth
        return (int)typeof(BHPlayerHeaalth).GetField("currentHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(playerHealth);
    }

}
