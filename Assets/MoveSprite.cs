using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    public Transform player;      // Reference to the player
    public Transform objectToMove; // Object that moves
    public Vector3 triggerPosition; // The position where movement starts
    public float targetX; // The forward target X position
    public float speed = 2f; // Lerp speed

    private float originalX; // The original X position
    private bool isMovingForward = false;
    private bool isMovingBackward = false;

    void Start()
    {
        originalX = objectToMove.position.x; // Store initial position
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, triggerPosition);

        // If player is near trigger and not already moving forward, start forward movement
        if (distance < 0.5f && !isMovingForward)
        {
            isMovingForward = true;
            isMovingBackward = false; // Stop backward movement if re-entering
        }

        // If player moves away and not already moving backward, start moving back
        if (distance > 0.6f && !isMovingBackward)
        {
            isMovingForward = false;
            isMovingBackward = true;
        }

        // Move object forward
        if (isMovingForward)
        {
            float newX = Mathf.Lerp(objectToMove.position.x, targetX, Time.deltaTime * speed);
            objectToMove.position = new Vector3(newX, objectToMove.position.y, objectToMove.position.z);

            if (Mathf.Abs(objectToMove.position.x - targetX) < 0.01f)
            {
                objectToMove.position = new Vector3(targetX, objectToMove.position.y, objectToMove.position.z);
                isMovingForward = false;
            }
        }

        // Move object back to original position
        if (isMovingBackward)
        {
            float newX = Mathf.Lerp(objectToMove.position.x, originalX, Time.deltaTime * speed);
            objectToMove.position = new Vector3(newX, objectToMove.position.y, objectToMove.position.z);

            if (Mathf.Abs(objectToMove.position.x - originalX) < 0.01f)
            {
                objectToMove.position = new Vector3(originalX, objectToMove.position.y, objectToMove.position.z);
                isMovingBackward = false;
            }
        }
    }
}