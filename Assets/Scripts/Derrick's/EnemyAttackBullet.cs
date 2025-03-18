using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
