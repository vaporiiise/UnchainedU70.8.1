using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player; // Reference to the player's transform.
    public Vector2 minBounds; // Minimum X and Y coordinates the camera can reach.
    public Vector2 maxBounds; // Maximum X and Y coordinates the camera can reach.
    public float smoothTime = 0.2f; // Smooth time for camera movement.

    private Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player != null)
        {
            // Determine the camera's target position based on the player's position.
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            // Clamp the target position to stay within the specified boundaries.
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

            // Smoothly move the camera to the target position.
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }
    }

    // Draw Gizmos in the Scene view to visualize the camera boundaries.
    private void OnDrawGizmos()
    {
        // Set the color for the boundary Gizmos.
        Gizmos.color = Color.red;

        // Calculate the center and size of the boundary rectangle.
        Vector3 center = new Vector3((minBounds.x + maxBounds.x) / 2, (minBounds.y + maxBounds.y) / 2, 0);
        Vector3 size = new Vector3(maxBounds.x - minBounds.x, maxBounds.y - minBounds.y, 1);

        // Draw a wireframe cube to represent the boundaries.
        Gizmos.DrawWireCube(center, size);
    }
}
