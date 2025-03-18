using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPattern : MonoBehaviour
{
    public GameObject bulletPrefab;            // Reference to the bullet prefab
    public float patternSwitchInterval = 5f;   // Time between switching patterns
    public float bulletSpawnInterval = 0.2f;   // Interval between spawning bullets

    private List<System.Action> bulletPatterns; // List to hold different bullet pattern methods
    private System.Action currentPattern;       // Current pattern being executed
    private float patternTimer;
    private float bulletTimer;                  // Timer for controlling bullet spawning rate

    void Start()
    {
        // Initialize the list of bullet patterns
        bulletPatterns = new List<System.Action>()
        {
            SpawnCircularPattern,
            SpawnSpiralPattern,
            SpawnWavePattern,
            SpawnRandomPattern
        };

        // Set the initial pattern randomly
        SelectRandomPattern();

        // Reset timers
        patternTimer = patternSwitchInterval;
        bulletTimer = bulletSpawnInterval;
    }

    void Update()
    {
        // Update the bullet timer
        bulletTimer -= Time.deltaTime;

        // If the bullet timer reaches zero, spawn a bullet for the current pattern
        if (bulletTimer <= 0f)
        {
            currentPattern?.Invoke();
            bulletTimer = bulletSpawnInterval; // Reset bullet timer
        }

        // Update the pattern switch timer
        patternTimer -= Time.deltaTime;

        // If the pattern timer reaches zero, switch to a new pattern
        if (patternTimer <= 0f)
        {
            SelectRandomPattern(); // Select a new random pattern
            patternTimer = patternSwitchInterval; // Reset the pattern switch timer
        }
    }

    // Method to select a random pattern from the list
    void SelectRandomPattern()
    {
        int randomIndex = Random.Range(0, bulletPatterns.Count);
        currentPattern = bulletPatterns[randomIndex];
    }

    // Pattern 1: Circular pattern
    void SpawnCircularPattern()
    {
        int bulletCount = 8; // Number of bullets to spawn in the pattern
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().direction = direction;
        }
    }

    // Pattern 2: Spiral pattern
    void SpawnSpiralPattern()
    {
        float spiralSpeed = 50f; // Speed at which the spiral rotates
        float angle = Time.time * spiralSpeed; // Time-dependent angle for spiral effect
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().direction = direction;
    }

    // Pattern 3: Wave pattern
    void SpawnWavePattern()
    {
        int bulletCount = 5;
        float waveSpread = 2f;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 spawnPosition = transform.position + new Vector3(-waveSpread / 2 + (i * waveSpread / bulletCount), 0, 0);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().direction = Vector2.down;
        }
    }

    // Pattern 4: Random pattern
    void SpawnRandomPattern()
    {
        int bulletCount = Random.Range(5, 20);
        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().direction = direction;
        }
    }
}
