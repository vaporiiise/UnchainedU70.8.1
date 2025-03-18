using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVBullet : MonoBehaviour
{
    public Vector2Int gridDirection; // The direction the bullet moves in (e.g., (1, 0) for right)
    public float gridSize = 1f;      // Size of a single grid cell
    public float moveDuration = 0.2f; // Time to move between grid cells

    private bool isMoving = false;

    public void SetDirection(Vector2Int direction)
    {
        gridDirection = direction;
    }

    void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveBullet());
        }
    }

    private IEnumerator MoveBullet()
    {
        isMoving = true;

        // Calculate target position
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = currentPosition + new Vector2(gridDirection.x, gridDirection.y) * gridSize;

        // Move the bullet smoothly
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector2.Lerp(currentPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Snap to the exact position to avoid drifting
        transform.position = targetPosition;

        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle collision (optional)
        if (collision.CompareTag("Wall") || collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Destroy the bullet on collision
        }
    }
}