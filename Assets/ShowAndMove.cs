using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndMove : MonoBehaviour
{
    public string targetTag = "ObjToPush";  // Tag of the first object
    public Transform player;  // Reference to the player
    public float threshold = 0.5f;  // Distance threshold for sprite visibility
    public float moveThreshold = 1.0f;  // Distance threshold for movement
    public GameObject spriteObject; // The sprite that appears
    public GameObject movingSprite; // The sprite that moves on the Y-axis
    public float moveSpeed = 2.0f; // Speed of movement
    public float moveDistance = 2.0f; // How far the moving sprite moves on the Y-axis

    private Vector3 originalMovePosition;
    private bool isNear = false;

    void Start()
    {
        if (movingSprite != null)
        {
            originalMovePosition = movingSprite.transform.position;
        }
    }

    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        bool playerClose = false;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(target.transform.position, player.position);

            if (distance <= threshold)
            {
                playerClose = true;
            }
        }

        if (playerClose != isNear)
        {
            isNear = playerClose;
            spriteObject.SetActive(isNear); // Show/hide the spriteObject
        }

        MoveSprite(playerClose);
    }

    void MoveSprite(bool move)
    {
        if (movingSprite == null) return;

        float targetY = move ? originalMovePosition.y + moveDistance : originalMovePosition.y;
        Vector3 targetPosition = new Vector3(originalMovePosition.x, targetY, originalMovePosition.z);

        movingSprite.transform.position = Vector3.Lerp(
            movingSprite.transform.position, 
            targetPosition, 
            Time.deltaTime * moveSpeed
        );
    }
}