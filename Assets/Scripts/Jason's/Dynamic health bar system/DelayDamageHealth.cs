using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayDamageHealth : MonoBehaviour
{
    public Slider mainHealthBarSlider;
    public Slider damagedHealthBarSlider;

    public bossHealth enemyHealth; // Reference to bossHealth script

    public float delayTime = 1f;        // Delay time before the delayed health bar starts following
    public float smoothSpeed = 2f;      // Higher = faster | Lower = smoother

    private float previousHealth;
    private float damageTimer;
    private bool isDamageReceived;

    private void Start()
    {
        if (mainHealthBarSlider != null && enemyHealth != null)
        {
            mainHealthBarSlider.maxValue = enemyHealth.maxHealth;
            damagedHealthBarSlider.maxValue = enemyHealth.maxHealth;
        }
        else
        {
            Debug.LogError("Missing reference to mainHealthBarSlider or enemyHealth.");
        }

        mainHealthBarSlider.value = enemyHealth.currentHealth;
        damagedHealthBarSlider.value = enemyHealth.currentHealth;
        previousHealth = enemyHealth.currentHealth;
        damageTimer = delayTime;
        isDamageReceived = false;
    }

    private void Update()
    {
        if (mainHealthBarSlider == null || damagedHealthBarSlider == null || enemyHealth == null)
        {
            Debug.LogError("Missing reference to sliders or enemyHealth.");
            return;
        }

        // Sync main health bar with the boss' actual health
        mainHealthBarSlider.value = enemyHealth.currentHealth;

        if (enemyHealth.currentHealth < previousHealth)
        {
            isDamageReceived = true;
            damageTimer = delayTime; // Reset delay timer when taking damage
        }
        else
        {
            isDamageReceived = false;
        }

        if (!isDamageReceived)
        {
            damageTimer -= Time.deltaTime;
            damageTimer = Mathf.Max(damageTimer, 0f);

            if (damageTimer <= 0f)
            {
                StartCoroutine(SmoothHealthUpdate());
            }
        }

        previousHealth = enemyHealth.currentHealth;
    }

    private IEnumerator SmoothHealthUpdate()
    {
        float startHealth = damagedHealthBarSlider.value;
        float targetHealth = mainHealthBarSlider.value;
        float elapsedTime = 0f;

        while (Mathf.Abs(damagedHealthBarSlider.value - targetHealth) > 0.01f)
        {
            elapsedTime += Time.deltaTime;
            float lerpValue = elapsedTime * smoothSpeed;

            damagedHealthBarSlider.value = Mathf.Lerp(startHealth, targetHealth, lerpValue);

            yield return null;
        }

        damagedHealthBarSlider.value = targetHealth;
    }

}