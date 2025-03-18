using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerAttack : MonoBehaviour
{
    public int currentCombo = 0;
    public float comboResetTime = 0.8F;
    public bool canContinueCombo = true;
    private float comboDuration;

    public GameObject attack1HitBox;
    public GameObject attack2HitBox;
    public GameObject attack3HitBox;
    public GameObject attack4HitBox;

    public HealthBar healthBar;
    public GameObject HealthBar;
    public int maxHealth = 100;
    public int currentHealth;

    public Animator playerAnim;

    private playerMovement movementScript;

    private float damageCooldown = 1.5F;
    private float lastDamageTime;

    public Transform playerTransform;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        movementScript = GetComponent<playerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentCombo < 4)
        {
            HandleCombo();
            HandleAnimation();
        }

        if (canContinueCombo && Time.time - comboDuration > comboResetTime)
            ResetCombo();

        HandlePause();
    }

    private void HandleCombo()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector2 direction = (mousePosition - transform.position).normalized;

        canContinueCombo = true;
        movementScript.EnableMovement(false);

        if (currentCombo > 4)
            return;

        if (Input.GetMouseButtonDown(0) && canContinueCombo)
        {
            comboDuration = Time.time;
            currentCombo++;
            currentCombo = Mathf.Clamp(currentCombo, 0, 4);

            switch (currentCombo)
            {
                case 1:
                    StartCoroutine(Attack1HitBox(direction));
                    break;
                case 2:
                    StartCoroutine(Attack2HitBox(direction));
                    break;
                case 3:
                    StartCoroutine(Attack3HitBox(direction));
                    break;
                case 4:
                    StartCoroutine(Attack4HitBox(direction));
                    break;
            }
        }
    }

    private void HandleAnimation()
    {
        ResetBool(playerAnim);

        if (currentCombo > 0)
            playerAnim.SetBool("PlayerCombo1", true);
        if (currentCombo > 1)
            playerAnim.SetBool("PlayerCombo2", true);
        if (currentCombo > 2)
            playerAnim.SetBool("PlayerCombo3", true);
        if (currentCombo > 3)
        {
            playerAnim.SetBool("PlayerCombo4", true);
            StartCoroutine(ReturnToIdle(0.8F));
        }
    }

    private void ResetCombo()
    {
        ResetBool(playerAnim);
        currentCombo = 0;
        canContinueCombo = false;
        movementScript.EnableMovement(true);
    }

    private void ResetBool(Animator playerAnimator)
    {
        foreach (AnimatorControllerParameter parameter in playerAnimator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
                playerAnimator.SetBool(parameter.name, false);
        }
    }

    IEnumerator ReturnToIdle(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetCombo();
    }

    IEnumerator Attack1HitBox(Vector2 attackDirection)
    {
        attack1HitBox.SetActive(true);
        attack1HitBox.transform.position = (Vector2)transform.position + attackDirection * 3.0F;
        yield return new WaitForSeconds(0.2F);
        attack1HitBox.SetActive(false);
    }

    IEnumerator Attack2HitBox(Vector2 attackDirection)
    {
        attack2HitBox.SetActive(true);
        attack2HitBox.transform.position = (Vector2)transform.position + attackDirection * 1.0F;
        float startAngle = 45F;
        float endAngle = -45F;

        float elapsedTime = 0F;
        while (elapsedTime < 0.2F)
        {
            float currentAngle = Mathf.Lerp(startAngle, endAngle, elapsedTime / 0.2F);
            attack2HitBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        attack2HitBox.SetActive(false);
    }

    IEnumerator Attack3HitBox(Vector2 attackDirection)
    {
        attack3HitBox.SetActive(true);
        attack3HitBox.transform.position = (Vector2)transform.position + attackDirection * 1.0F;
        float startAngle = -45F;
        float endAngle = 45F;

        float elapsedTime = 0F;
        while (elapsedTime < 0.2F)
        {
            float currentAngle = Mathf.Lerp(startAngle, endAngle, elapsedTime / 0.2F);
            attack3HitBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        attack3HitBox.SetActive(false);
    }

    IEnumerator Attack4HitBox(Vector2 attackDirection)
    {
        attack4HitBox.SetActive(true);
        attack4HitBox.transform.position = (Vector2)transform.position + attackDirection * 1.0F;
        yield return new WaitForSeconds(0.2F);
        attack4HitBox.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D playerCol)
    {
        if (playerCol.gameObject.CompareTag("BossAttack") && Time.time > lastDamageTime + damageCooldown)
        {
            TakeDamage(5);
            lastDamageTime = Time.time;
        }
        healthBar.SetHealth(currentHealth);

    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Player Health: {currentHealth}");
        healthBar.SetHealth(currentHealth);

        if (maxHealth <= 0)
            Die();
    }

    public void HealDamage(int heal)
    {
        Debug.Log("Player Healed!");
        currentHealth += heal;
        Debug.Log($"Player Health: {currentHealth}");
        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        SceneManager.LoadScene(2);
    }

    private void HandlePause()
    {
        if (PauseMenu.GameIsPaused)
        {
            return;
        }
    }
}