using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NHenemyHealth : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;
    public AudioClip takeDamageSound;
    private AudioSource audioSource;

    public GameObject[] healthSprites; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
        Debug.Log("Enemy Health: " + currentHealth);
        audioSource.PlayOneShot(takeDamageSound);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        int remainingHealthSprites = currentHealth / 10;

        for (int i = 0; i < healthSprites.Length; i++)
        {
            if (i < remainingHealthSprites)
            {
                healthSprites[i].SetActive(true); 
            }
            else
            {
                healthSprites[i].SetActive(false); 
            }
        }
    }

    void Die()
    {
        Debug.Log("Enemy defeated!");
        Destroy(gameObject);
    }
}
