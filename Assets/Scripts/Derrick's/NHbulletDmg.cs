using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NHbulletDmg : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            NHenemyHealth enemy = other.GetComponent<NHenemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); 
        }
    }
}
