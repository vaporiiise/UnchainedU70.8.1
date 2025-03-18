using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTest : MonoBehaviour
{
    public int bossMaxhealth = 40;
    private void OnTriggerEnter2D(Collider2D enemyCol)
    {
        if (enemyCol.gameObject.CompareTag("PlayerAttack"))
            TakeDamage(10);
    }

    private void TakeDamage(int damage)
    {
        bossMaxhealth -= damage;
        Debug.Log($"Health remaining: {bossMaxhealth}");
        if (bossMaxhealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}