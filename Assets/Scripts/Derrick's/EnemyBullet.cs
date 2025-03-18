using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f; // Speed of the bullet
    public Vector2 direction = Vector2.up; // Initial direction of the bullet

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroy bullet when it goes off-screen
    }
}