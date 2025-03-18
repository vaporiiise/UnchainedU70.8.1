using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform.
    public float smoothSpeed = 0.125f; // Smooth speed for camera movement.
    public Vector3 offset; // Offset for the camera position.

    private void LateUpdate()
    {
        // Calculate desired position.
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate between current position and desired position.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position.
        transform.position = smoothedPosition;
    }
}