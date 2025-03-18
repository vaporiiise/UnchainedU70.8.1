using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomTrigger : MonoBehaviour
{
    public Transform player; // Reference to the player's transform.
    public float defaultFOV = 60f;  // Default field of view of the camera.
    public float zoomOutFOV = 45f;   // Field of view when zoomed out.
    public float zoomSpeed = 2f;      // Speed at which the camera zooms in/out.

    // Define the area where the camera should zoom out.
    public Vector2 zoomAreaMin = new Vector2(-5, -5); // Bottom-left corner of the area.
    public Vector2 zoomAreaMax = new Vector2(5, 5);   // Top-right corner of the area.

    private Camera cam;

    private void Start()
    {
        cam = Camera.main; // Get the main camera.
        cam.fieldOfView = defaultFOV; // Set initial FOV level.
    }

    private void Update()
    {
        // Check if the player is within the defined zoom area.
        if (player.position.x >= zoomAreaMin.x && player.position.x <= zoomAreaMax.x &&
            player.position.y >= zoomAreaMin.y && player.position.y <= zoomAreaMax.y)
        {
            // Smoothly zoom out the camera if the player is within the zoom area.
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomOutFOV, zoomSpeed * Time.deltaTime);
        }
        else
        {
            // Smoothly zoom back to the default size if the player is outside the zoom area.
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultFOV, zoomSpeed * Time.deltaTime);
        }
    }

    // Optional: Draw the zoom area in the Scene view for visualization.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(zoomAreaMin.x, zoomAreaMin.y, 0), new Vector3(zoomAreaMax.x, zoomAreaMin.y, 0));
        Gizmos.DrawLine(new Vector3(zoomAreaMin.x, zoomAreaMin.y, 0), new Vector3(zoomAreaMin.x, zoomAreaMax.y, 0));
        Gizmos.DrawLine(new Vector3(zoomAreaMax.x, zoomAreaMin.y, 0), new Vector3(zoomAreaMax.x, zoomAreaMax.y, 0));
        Gizmos.DrawLine(new Vector3(zoomAreaMin.x, zoomAreaMax.y, 0), new Vector3(zoomAreaMax.x, zoomAreaMax.y, 0));
    }
}