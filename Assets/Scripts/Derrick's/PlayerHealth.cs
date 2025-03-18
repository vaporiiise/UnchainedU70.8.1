using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int Maxhealth = 100;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BossAttack"))
            TakeDamage(10);
    }

    private void TakeDamage(int damage)
    {
        Maxhealth -= damage;
        Debug.Log($"Health remaining: {Maxhealth}");
        if (Maxhealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}