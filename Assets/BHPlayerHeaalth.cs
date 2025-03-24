using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BHPlayerHeaalth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 30;
    private int currentHealth;

    [Header("Audio Settings")]
    public AudioClip takeDamageSound;
    public AudioClip healSound;
    public AudioSource audioSource;  // Now you can assign an AudioSource from the Inspector

    [Header("UI Elements")]
    public List<Image> healthImages;
    public GameObject damageIndicator;
    public float damageDisplayDuration = 0.5f;

    [Header("Death Screen")]
    public GameObject deathCanvas;
    public List<GameObject> otherCanvases;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (damageIndicator != null)
        {
            damageIndicator.gameObject.SetActive(false);
        }

        if (deathCanvas != null)
        {
            deathCanvas.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        Debug.Log("Player Health: " + currentHealth);

        if (audioSource != null && takeDamageSound != null)
        {
            audioSource.PlayOneShot(takeDamageSound);
        }

        if (damageIndicator != null)
        {
            StartCoroutine(ShowDamageIndicator());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator ShowDamageIndicator()
    {
        damageIndicator.gameObject.SetActive(true);
        yield return new WaitForSeconds(damageDisplayDuration);
        damageIndicator.gameObject.SetActive(false);
    }

    void UpdateHealthUI()
    {
        int healthPerImage = maxHealth / healthImages.Count;
        int remainingSprites = currentHealth / healthPerImage;
        remainingSprites = Mathf.Clamp(remainingSprites, 0, healthImages.Count);

        for (int i = 0; i < healthImages.Count; i++)
        {
            healthImages[i].gameObject.SetActive(i < remainingSprites);
        }
    }

    void Die()
    {
        Debug.Log("Player is dead!");

        AudioListener.pause = true;
        Time.timeScale = 0;

        if (deathCanvas != null)
        {
            deathCanvas.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Death canvas is not assigned in the inspector!");
        }

        foreach (GameObject canvas in otherCanvases)
        {
            if (canvas != null)
            {
                canvas.SetActive(false);
            }
        }
    }
}
