using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public Transform player; // Reference to the player

    public Vector2 minPosition; // Minimum x, y position for the area
    public Vector2 maxPosition; // Maximum x, y position for the area
    public Vector3 lockedCameraPosition; // The position to lock the camera to
    public float lockedCameraSize = 5f; // Zoom size when the camera is locked

    public float transitionSpeed = 2f; // Speed for smooth transitions

    private Camera mainCamera; // Reference to the main camera
    private bool isCameraLocked = false; // Check if the camera is locked
    private float originalCameraSize; // Store the original camera size
    private Vector3 originalCameraPosition; // Store the original camera position
    private float smoothTime = 0.3f; // Time for smooth transitions

    //public MonoBehaviour scriptToDisable; // Reference to the script that should be disabled

    void Start()
    {
        mainCamera = Camera.main; // Get the main camera
        originalCameraSize = mainCamera.orthographicSize; // Store the original zoom size
    }

    void LateUpdate()
    {
        // Check if the player is inside the defined area
        if (!isCameraLocked && player.position.x >= minPosition.x && player.position.x <= maxPosition.x &&
            player.position.y >= minPosition.y && player.position.y <= maxPosition.y)
        {
            // Lock the camera to the specified position with smooth transition
            LockCamera();
        }
        else if (isCameraLocked && (player.position.x < minPosition.x || player.position.x > maxPosition.x ||
            player.position.y < minPosition.y || player.position.y > maxPosition.y))
        {
            // Unlock the camera and smoothly transition it back to follow the player
            UnlockCamera();
        }

        // Smoothly transition camera position and zoom if locked
        if (isCameraLocked)
        {
            SmoothTransition(lockedCameraPosition, lockedCameraSize);
        }
        else
        {
            SmoothTransition(originalCameraPosition, originalCameraSize);
        }
    }

    void LockCamera()
    {
        isCameraLocked = true;
        originalCameraPosition = mainCamera.transform.position; // Store the original camera position

       
    }

    void UnlockCamera()
    {
        isCameraLocked = false;

       
    }

    void SmoothTransition(Vector3 targetPosition, float targetSize)
    {
        // Smoothly transition the camera position
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(targetPosition.x, targetPosition.y, mainCamera.transform.position.z), transitionSpeed * Time.deltaTime);

        // Smoothly transition the camera zoom (orthographic size)
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, transitionSpeed * Time.deltaTime);
    }

    // Draw Gizmos to visualize the min/max area in the Scene view
    private void OnDrawGizmos()
    {
        // Set the color for the Gizmos
        Gizmos.color = Color.yellow;

        // Draw a wireframe box representing the area
        Vector2 size = new Vector2(maxPosition.x - minPosition.x, maxPosition.y - minPosition.y);
        Vector3 center = new Vector3((minPosition.x + maxPosition.x) / 2, (minPosition.y + maxPosition.y) / 2, 0);

        // Draw a wire cube in the scene view representing the lock area
        Gizmos.DrawWireCube(center, size);
    }
}