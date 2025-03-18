using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDevBossHall : MonoBehaviour
{
    public Transform spriteToMove;  // The sprite that moves
    public Transform player;        // The player reference
    public Vector2[] moveAreas;     // Positions where animation + movement trigger
    public Vector2[] targetPositions; // Target positions for each area
    public float moveSpeed = 2f;    // Speed of movement
    public float detectionRadius = 2f; // Distance to detect the player
    public string triggerAnimation = "BossPicAnim"; // Animation trigger name

    private bool isMoving = false;
    private Animator animator;

    private void Start()
    {
        animator = spriteToMove.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            for (int i = 0; i < moveAreas.Length; i++)
            {
                if (Vector2.Distance(player.position, moveAreas[i]) < detectionRadius)
                {
                    StartCoroutine(MoveSprite(targetPositions[i]));
                    break; // Prevent multiple triggers at once
                }
            }
        }
    }

    IEnumerator MoveSprite(Vector2 newPosition)
    {
        isMoving = true;

        // Play animation when the player enters an area
        if (animator != null)
        {
            animator.SetTrigger(triggerAnimation);
        }

        Vector2 startPos = spriteToMove.position;
        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * moveSpeed;
            spriteToMove.position = Vector2.Lerp(startPos, newPosition, time);
            yield return null;
        }

        spriteToMove.position = newPosition; // Ensure exact position

        isMoving = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector2 area in moveAreas)
        {
            Gizmos.DrawWireSphere(area, detectionRadius);
        }
    }
}