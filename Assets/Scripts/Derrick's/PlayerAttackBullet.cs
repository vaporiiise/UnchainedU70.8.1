using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
