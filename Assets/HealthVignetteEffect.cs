using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVignetteEffect : MonoBehaviour
{
    [Header("References")]
    public HeartBeaterManager heartBeater; // Drag & Drop HeartBeaterManager
    public BHPlayerHeaalth playerHealth;  // Drag & Drop PlayerHealth Script
    public Image vignetteImage; // Drag & Drop the red overlay image

    [Header("Vignette Settings")]
    public float maxAlpha = 0.5f;  // Max red intensity
    public float fadeSpeed = 2f;   // How fast it fades in/out

    [Header("Pulsing Settings")]
    public float pulseStrength = 0.15f; // How strong the pulse effect is
    public float pulseSpeedMultiplier = 1f; // Adjust pulse speed manually

    [Header("Pulse Pattern Settings")]
    public int beatsPerCycle = 2; // How many beats before a pause
    public float pauseDuration = 0.5f; // How long the pause lasts

    private float targetAlpha = 0f; // The desired alpha (depends on health)
    private float pulseTimer = 0f; // Tracks heartbeat time
    private int currentBeat = 0; // Tracks beats in the cycle
    private bool isPaused = false;
    private float pauseTimer = 0f;
    private float heartbeatInterval; // Time between heartbeats

    private void Start()
    {
        if (vignetteImage == null)
        {
            Debug.LogError("Vignette Image is not assigned in the Inspector!");
            return;
        }

        if (heartBeater == null)
        {
            heartBeater = FindObjectOfType<HeartBeaterManager>();
        }

        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<BHPlayerHeaalth>();
        }

        vignetteImage.color = new Color(1f, 0f, 0f, 0f); // Fully transparent at start
    }

    private void Update()
    {
        if (playerHealth == null || vignetteImage == null || heartBeater == null || heartBeater.audioSource == null)
            return;

        int currentHealth = playerHealth.GetCurrentHealth();
        bool shouldShowVignette = heartBeater.audioSource.isPlaying;

        if (shouldShowVignette)
        {
            heartbeatInterval = 60f / heartBeater.audioSource.pitch; // Sync with heartbeat speed
            float healthPercent = (float)currentHealth / playerHealth.maxHealth;
            targetAlpha = Mathf.Lerp(maxAlpha, 0f, healthPercent);

            Color currentColor = vignetteImage.color;
            currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, Time.deltaTime * fadeSpeed);
            vignetteImage.color = currentColor;

            if (!isPaused)
            {
                pulseTimer += Time.deltaTime;
                float pulseAlpha = Mathf.Abs(Mathf.Sin(pulseTimer * Mathf.PI * 2f)) * pulseStrength;

                vignetteImage.color = new Color(1f, 0f, 0f, Mathf.Clamp(targetAlpha + pulseAlpha, 0f, maxAlpha));

                if (pulseTimer >= heartbeatInterval * 0.5f)
                {
                    pulseTimer = 0f;
                    currentBeat++;

                    if (currentBeat >= beatsPerCycle)
                    {
                        isPaused = true;
                        pauseTimer = 0f;
                        currentBeat = 0;
                    }
                }
            }
            else
            {
                pauseTimer += Time.deltaTime;
                if (pauseTimer >= pauseDuration)
                {
                    isPaused = false;
                }
            }
        }
        else
        {
            Color currentColor = vignetteImage.color;
            currentColor.a = Mathf.Lerp(currentColor.a, 0f, Time.deltaTime * fadeSpeed);
            vignetteImage.color = currentColor;
        }
    }
}
