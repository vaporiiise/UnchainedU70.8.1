using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BHPlayerHeaalth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;
    public AudioClip takeDamageSound;
    private AudioSource audioSource;
    public AudioClip healSound;

    public List<Image> healthImages;
    public GameObject damageIndicator;
    public float damageDisplayDuration = 0.5f;

    public GameObject deathCanvas;
    public List<GameObject> otherCanvases;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        UpdateHealthUI();

        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);
        audioSource.PlayOneShot(takeDamageSound);
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
        int healthPerImage = maxHealth / healthImages.Count; // Health each image represents
        int remainingSprites = currentHealth / healthPerImage; // How many images should be active?

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

        // Disable other canvases
        foreach (GameObject canvas in otherCanvases)
        {
            if (canvas != null)
            {
                canvas.SetActive(false);
            }
        }

    }
}