using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NHEnemyAttack : MonoBehaviour
{
    public float attackRange = 2f;          
    public float attackCooldown = 1.5f;    
    public int damageAmount = 10;          

    private Transform player;              
    private Animator animator;             
    private float nextAttackTime = 0f;     

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        animator = GetComponent<Animator>();                          
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + attackCooldown; 
                }
            }
        }
    }

    void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        player.GetComponent<NHPlayerHealth>().TakeDamage(damageAmount);

        Debug.Log("Enemy attacked!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
