using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubtleShake : MonoBehaviour
{
    public float shakeDuration = 1f;  // Total duration of the shake
    public float shakeAmount = 0.2f;  // How much to shake (lower is more subtle)
    public float shakeSpeed = 50f;    // How fast the shake oscillates

    private RectTransform rectTransform;
    private Vector2 originalPos;
    private float timer;

    void Awake()
    {
        // Get the RectTransform of the UI TextMeshPro
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        // Automatically start shaking as soon as the game starts
        originalPos = rectTransform.anchoredPosition;
        timer = shakeDuration;
    }

    void Update()
    {
        if (timer > 0)
        {
            // Create smooth shake motion with Perlin noise (more subtle)
            float x = (Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) - 0.5f) * shakeAmount;
            float y = (Mathf.PerlinNoise(0f, Time.time * shakeSpeed) - 0.5f) * shakeAmount;

            // Apply the shake offset to the original position
            rectTransform.anchoredPosition = originalPos + new Vector2(x, y);

            timer -= Time.deltaTime;  // Decrease timer as time passes
        }
        else if (rectTransform.anchoredPosition != originalPos)
        {
            // Ensure the text resets to the original position at the end
            rectTransform.anchoredPosition = originalPos;
        }
    }
}