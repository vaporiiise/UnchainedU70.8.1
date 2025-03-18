using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeNFillHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public GameObject canvasObject;
    private CanvasGroup canvasGroup;
    public float fadeDuration = 1f;
    public float fillDuration = 2f;

    private float targetHealth;
    private float currentHealth;
    private bool isFadingIn;
    private bool isFilling;

    void Start()
    {
        // Initialize CanvasGroup and set initial health
        canvasGroup = canvasObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;  // Start with health bar hidden

        targetHealth = 0;
        currentHealth = 0;
        healthSlider.value = 0;

        // Fade in the health bar
        StartCoroutine(FadeInHealthBar());
    }

    void Update()
    {
        // If health is less than targetHealth, fill up the health bar
        if (currentHealth < targetHealth && isFilling)
        {
            currentHealth += Time.deltaTime * (targetHealth / fillDuration);
            healthSlider.value = currentHealth / targetHealth;
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        targetHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        currentHealth = 0;  // Start from 0
        healthSlider.value = 0;

        // Start filling up
        StartCoroutine(FillHealthBar());
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
        healthSlider.value = health / targetHealth;
    }

    // Fade in the health bar over time
    IEnumerator FadeInHealthBar()
    {
        isFadingIn = true;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f; // Ensure it's fully visible at the end
        isFadingIn = false;
        isFilling = true; // Start filling the health bar after fade-in
    }

    // slowly fill from 0 to de max
    IEnumerator FillHealthBar()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fillDuration)
        {
            healthSlider.value = Mathf.Lerp(0f, targetHealth, elapsedTime / fillDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        healthSlider.value = targetHealth;  // Ensure it's fully filled at the end
    }

    // When the boss takes damage dis shet will be called
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        SetHealth(currentHealth);
    }

    // Fade out the health bar
    public void FadeOutHealthBar()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}