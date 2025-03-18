using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BossState
{
    Idle,
    Move,
    Attack,
    Cooldown
}

public class bossAI : MonoBehaviour
{
    public BossState currentState = BossState.Idle;
    public Transform player;
    public float moveSpeed = 3F;
    public float attackRange = 4F;
    public float attackCooldown = 60F;
    private float attackCooldownTimer;
    public int attackPattern;
    public int lastPattern;

    public GameObject handSlamHitBox;
    public GameObject groundSlamHitBox;
    public GameObject handSwipeHitBox;

    public int bossMaxHealth = 300;
    public int currentHealth;
    public HealthBar healthBar;
    public GameObject HealthBar;
    public Animator bossAnim;

    private float damageCooldown = 0.1F;
    private float lastDamageTime;

    private void Start()
    {
        currentHealth = bossMaxHealth;
        healthBar.SetMaxHealth(bossMaxHealth);
     
    }

    private void Update()
    {

        switch (currentState)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Move:
                Move();
                break;
            case BossState.Attack:
                Attack();
                break;
            case BossState.Cooldown:
                Cooldown();
                break;
        }
    }

    private void Idle()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange)
            currentState = BossState.Attack;
        else
            currentState = BossState.Move;
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < attackRange)
            currentState = BossState.Attack;
    }

    private void Attack()
    {
        do
        {
            attackPattern = Random.Range(1, 4);
        } while (attackPattern == lastPattern);

        lastPattern = attackPattern;

        if (attackPattern == 1)
        {
            StartCoroutine(HandSlam());
            bossAnim.SetBool("fastPound", true);
        }
        else if (attackPattern == 2)
        {
            StartCoroutine(GroundSlam());
            bossAnim.SetBool("groundPound", true);
        }   
        else if (attackPattern == 3)
        {
            StartCoroutine(JumpAttack());
            bossAnim.SetBool("jumpAttack", true);
        }  

        currentState = BossState.Cooldown;
        attackCooldownTimer = attackCooldown;

        StartCoroutine(ReturnToIdle(1F));
    }

    private void Cooldown()
    {
        attackCooldownTimer -= Time.deltaTime;

        if (attackCooldownTimer <= 0)
            currentState = BossState.Idle;
    }

    private void ResetBool(Animator bossAnimator)
    {
        foreach (AnimatorControllerParameter parameter in bossAnimator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                bossAnimator.SetBool(parameter.name, false);
        }
    }

    IEnumerator ReturnToIdle(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetBool(bossAnim);
    }

    IEnumerator HandSlam()
    {
        Debug.Log("Hand Slam!");

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 attackPosition = (Vector2)transform.position + directionToPlayer * 1.5F;
        handSlamHitBox.transform.position = attackPosition;

        yield return new WaitForSeconds(1F);
        handSlamHitBox.SetActive(true);
        yield return new WaitForSeconds(0.2F);
        handSlamHitBox.SetActive(false);
    }

    IEnumerator GroundSlam()
    {
        Debug.Log("Ground Slam!");

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 attackPosition = (Vector2)transform.position + directionToPlayer * 1.5F;
        groundSlamHitBox.transform.position = attackPosition;

        yield return new WaitForSeconds(0.8F);
        groundSlamHitBox.SetActive(true);
        yield return new WaitForSeconds(0.2F);
        groundSlamHitBox.SetActive(false);
    }

    IEnumerator JumpAttack()
    {
        Debug.Log("Jump Attack!");

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 attackPosition = (Vector2)transform.position + directionToPlayer * 1.5F;
        handSwipeHitBox.transform.position = attackPosition;

        float startAngle = -90F;
        float endAngle = 90F;

        float elapsedTime = 0F;
        while (elapsedTime < 0.2F)
        {
            float currentAngle = Mathf.Lerp(startAngle, endAngle, elapsedTime / 0.2F);
            handSwipeHitBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1F);
        handSwipeHitBox.SetActive(true);
        yield return new WaitForSeconds(0.2F);
        handSwipeHitBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D enemyCol)
    {
        if (enemyCol.gameObject.CompareTag("PlayerAttack") && Time.time > lastDamageTime + damageCooldown)
        {
            TakeDamage(10);
            lastDamageTime = Time.time;
        }

        healthBar.SetHealth(currentHealth);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Boss Health: {currentHealth}");
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        Destroy(HealthBar);
    }
}