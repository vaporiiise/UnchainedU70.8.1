using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float spawnInterval = 0.2f; // Time between each bullet spawn

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnBulletPattern();
            timer = 5f;
            SpawnSpiralPattern();
            timer = 5f;
            SpawnRandomPattern();
            timer = 5f;
            SpawnWavePattern();
        }
    }

    // Method to spawn bullets in a circular pattern
    void SpawnBulletPattern()
    {
        int bulletCount = 8; // Number of bullets to spawn in the pattern
        float angleStep = 360f / bulletCount; // Angle between each bullet

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep; // Calculate angle for each bullet
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().direction = direction;
        }
    }
    void SpawnSpiralPattern()
    {
        float angle = 0f; // Initial angle
        float angleStep = 10f; // Angle between bullets in the spiral

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        bullet.GetComponent<EnemyBullet>().direction = direction;

        angle += angleStep;
    }
    void SpawnWavePattern()
    {
        int bulletCount = 5; // Number of bullets per wave
        float waveSpread = 2f; // Distance between each bullet

        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 spawnPosition = transform.position + new Vector3(-waveSpread / 2 + (i * waveSpread / bulletCount), 0, 0);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().direction = Vector2.down; // All bullets move upwards in a wave
        }
    }
    void SpawnRandomPattern()
    {
        int bulletCount = Random.Range(5, 20); // Random number of bullets
        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 direction = Random.insideUnitCircle.normalized; // Random direction for each bullet

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().direction = direction;
        }
    }
}
