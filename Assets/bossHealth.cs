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
    public int sceneInt = 3;
    public ShakeHealthBar shakeHealthBar;
    public AudioClip tookDamage;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // Ensure the shakeHealthBar is assigned properly
        if (shakeHealthBar == null)
        {
            shakeHealthBar = FindObjectOfType<ShakeHealthBar>(); // Find in the scene
        }
        audioSource = GetComponent<AudioSource>();
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
            shakeHealthBar.HealthBarShake(); // Call shake function
        }
        else
        {
            Debug.LogWarning("ShakeHealthBar script is not assigned!");
        }
        audioSource.PlayOneShot(tookDamage);
    }

    public void Die()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(sceneInt);
        }
    }
}
