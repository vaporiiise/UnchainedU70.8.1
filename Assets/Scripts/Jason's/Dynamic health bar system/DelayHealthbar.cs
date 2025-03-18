using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayHealthbar : MonoBehaviour
{
    public Slider mainHealthBarSlider;
    public Slider damagedHealthBarSlider;

    public playerAttack playerScript;

    public float delayTime = 1f;        // Delay time before health bar starts following
    public float smoothSpeed = 2f;      // Higher = faster | Lower = Smoother

    private float targetHealth;
    private float previousHealth;
    private float damageTimer;
    private bool isDamageReceived;

    void Start() //
    {
        if (mainHealthBarSlider != null && playerScript != null)
        {
            mainHealthBarSlider.maxValue = playerScript.maxHealth;
            damagedHealthBarSlider.maxValue = playerScript.maxHealth;
        }
        else
        {
            Debug.LogError("Missing reference to mainHealthBarSlider(player) or playerScript.");
        }

        targetHealth = mainHealthBarSlider.value;
        previousHealth = mainHealthBarSlider.value;
        damageTimer = delayTime;
        isDamageReceived = false;
    }

    void Update() //
    {
        if (mainHealthBarSlider == null || damagedHealthBarSlider == null || playerScript == null)
        {
            Debug.LogError("Missing reference to sliders or playerScript.");
            return;
        }

        if (mainHealthBarSlider.value < previousHealth)
        {
            isDamageReceived = true;
            damageTimer = delayTime;
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

        previousHealth = mainHealthBarSlider.value;

        //Debug.Log($"Player Main Health: {mainHealthBarSlider.value}, Player Damaged Health: {damagedHealthBarSlider.value}, Player Damage Timer: {damageTimer}");
    }
    private IEnumerator SmoothHealthUpdate() //
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
