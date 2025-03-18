using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerHallsPlayerAnimator : MonoBehaviour
{
    private KeyCode lastKeyPressed;
    public float cD = 0.3f;
    public float inputWindowTime = 1f;

    public Animator anim;
    public bool isWalkingDown = false;
    public bool isWalkingLeft = false;
    public bool isWalkingRight = false;
    public bool isWalkingUp = false;

    public bool isAttacking = false;
    public int comboStep = 0;
    public bool canContinueCombo = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator component not found on this GameObject!");
        }
    }

    void Update()
    {
        // Check if the game is paused
        if (PauseMenu.GameIsPaused)
        {
            if (isAttacking)
            {
                anim.Play("Idle"); // Set to idle or whatever state you prefer
            }
            return; // Exit early to prevent further input processing
        }

        if (isAttacking) return; // Prevent further input while attacking

        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (comboStep == 0)
            {
                StartCoroutine(HandleCombo());
            }
            else if (canContinueCombo)
            {
                ContinueCombo();
            }
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.S))
        {
            isWalkingDown = true;
            anim.Play("WalkDown");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            isWalkingLeft = true;
            anim.Play("WalkLeft");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            isWalkingRight = true;
            anim.Play("WalkRight");
        }
        else if (Input.GetKey(KeyCode.W))
        {
            isWalkingUp = true;
            anim.Play("WalkUp");
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            anim.Play("UpIdle");
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            anim.Play("LeftIdle");
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            anim.Play("Idle");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            anim.Play("RightIdle");
        }
    }

    IEnumerator HandleCombo()
    {
        isAttacking = true;
        comboStep = 1;
        Debug.Log("Playing PlayerCombo1");
        anim.SetTrigger("PlayerCombo1");
        canContinueCombo = true;

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + cD);

        yield return new WaitForSeconds(inputWindowTime);

        canContinueCombo = false;
        ResetCombo();
    }

    private void ContinueCombo()
    {
        if (comboStep < 4)
        {
            comboStep++;
            Debug.Log("Continuing Combo, playing PlayerCombo" + comboStep);
            anim.SetTrigger("PlayerCombo" + comboStep);

            if (comboStep == 4)
            {
                StartCoroutine(FinishCombo());
            }
        }
    }

    IEnumerator FinishCombo()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + cD);
        ResetCombo();
    }

    private void ResetCombo()
    {
        comboStep = 0;
        isAttacking = false;
        anim.Play("Idle");
    }
}