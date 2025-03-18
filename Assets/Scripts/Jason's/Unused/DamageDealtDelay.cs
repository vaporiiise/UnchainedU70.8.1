using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDealtDelay : MonoBehaviour
{
    public Slider mainHealthBarSlider;     // Main health bar slider
    public Slider damagedHealthBarSlider;  // Damaged health bar slider

    // Reference to the bossAI script attached to the enemy
    public bossAI enemyScript;

    // Start is called before the first frame update
    private void Start()
    {
        // Ensure that the main health bar max value is set to the enemy's max health
        if (mainHealthBarSlider != null && enemyScript != null)
        {
            mainHealthBarSlider.maxValue = enemyScript.bossMaxHealth;
            damagedHealthBarSlider.maxValue = enemyScript.bossMaxHealth;
        }
        else
        {
            Debug.LogError("Missing reference to mainHealthBarSlider or enemyScript.");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if both sliders and the enemy script are properly assigned
        if (mainHealthBarSlider != null && damagedHealthBarSlider != null && enemyScript != null)
        {
            // Update the damaged health bar value to follow the main health bar value instantly
            damagedHealthBarSlider.value = mainHealthBarSlider.value;
        }
        else
        {
            Debug.LogError("Missing reference to sliders or enemyScript.");
        }
    }
}

