using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerTriggerButton : MonoBehaviour
{
    public Transform targetObject;    // The game object to move
    public Vector2 targetPosition;   // The target position in 2D space
    public float moveSpeed = 2f;     // Speed of the movement
    public string triggeringTag = "ButtonTrigger"; // The tag of the triggering object

    private bool shouldMove = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the triggering object matches the specified tag
        if (other.CompareTag(triggeringTag))
        {
            shouldMove = true;
        }
    }

    private void Update()
    {
        if (shouldMove)
        {
            // Smoothly move the object toward the target position
            targetObject.position = Vector2.Lerp(
                targetObject.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            // Stop moving when close to the target position
            if (Vector2.Distance(targetObject.position, targetPosition) < 0.01f)
            {
                targetObject.position = targetPosition; // Snap to exact position
                shouldMove = false;
            }
        }
    }
}
