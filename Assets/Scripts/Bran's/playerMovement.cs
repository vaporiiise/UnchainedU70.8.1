using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5F;
    private Collider2D playerCol;
    private Rigidbody2D playerRB;
    private bool canMove = true;

    private Vector2 movement;
    public float dodgeSpeed = 7F;
    public float dodgeDuration = 0.2F;
    public float dodgeCooldown = 3F;
    public bool isInvincible = false;

    private float lastDodgeTime;
    private bool isDodging = false;
    private Vector2 dodgeDirection;

    public Animator playerAnim;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerCol = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (canMove)
        {
            HandleMovement();
            HandleDodge();
            HandleAnimation();
        }
        
        HandlePause();
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
    }

    private void HandleMovement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void HandleDodge()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastDodgeTime + dodgeCooldown)
        {
            isDodging = true;
            lastDodgeTime = Time.time;
            dodgeDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            isInvincible = true;
            playerCol.enabled = false;
        }

        playerRB.MovePosition(playerRB.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

        if (isDodging)
        {
            playerRB.MovePosition(playerRB.position + dodgeDirection * dodgeSpeed * Time.fixedDeltaTime);

            if (Time.time > lastDodgeTime + dodgeDuration)
            {
                isDodging = false;
                isInvincible = false;
                playerCol.enabled = true;
            }
        }
    }

    private void HandleAnimation()
    {
        if (Input.GetKeyDown(KeyCode.W))
            playerAnim.SetBool("isWalkingUp", true);
        else if (Input.GetKeyDown(KeyCode.A))
            playerAnim.SetBool("isWalkingLeft", true);
        else if (Input.GetKeyDown(KeyCode.S))
            playerAnim.SetBool("isWalkingDown", true);
        else if (Input.GetKeyDown(KeyCode.D))
            playerAnim.SetBool("isWalkingRight", true);
        else if (Input.GetKeyUp(KeyCode.W))
            playerAnim.SetBool("isWalkingUp", false);
        else if (Input.GetKeyUp(KeyCode.A))
            playerAnim.SetBool("isWalkingLeft", false);
        else if (Input.GetKeyUp(KeyCode.S))
            playerAnim.SetBool("isWalkingDown", false);
        else if (Input.GetKeyUp(KeyCode.D))
            playerAnim.SetBool("isWalkingRight", false);
    }
    private void HandlePause()
    {
        if (PauseMenu.GameIsPaused)
            return;
    }
}