using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossHealth : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth;
    public HealthBar healthBar;
    public string sceneName = "BossHall";
    public ShakeHealthBar shakeHealthBar;
    public AudioClip tookDamage;
    private AudioSource audioSource;

    void Start()
    {
        if (PlayerPrefs.HasKey("SavedHealth"))
        {
            currentHealth = PlayerPrefs.GetInt("SavedHealth");
        }
        else
        {
            currentHealth = maxHealth;
        }

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth); // Keep the bar in sync

        if (shakeHealthBar == null)
        {
            shakeHealthBar = FindObjectOfType<ShakeHealthBar>();
        }

        audioSource = GetComponent<AudioSource>();

        Debug.Log("Health loaded: " + currentHealth);
    }


    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (shakeHealthBar != null)
        {
            shakeHealthBar.HealthBarShake();
        }
        else
        {
            Debug.LogWarning("ShakeHealthBar script is not assigned!");
        }

        if (tookDamage != null)
        {
            SFXManager.instance.PlaySFX(tookDamage);
        }

        //audioSource.PlayOneShot(tookDamage);
    }

    public void Die()
    {
        if (currentHealth <= 0)
        {
            // Save current health before scene change
            if (BossHealthGameManager.Instance != null)
            {
                BossHealthGameManager.Instance.savedHealth = currentHealth;
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}