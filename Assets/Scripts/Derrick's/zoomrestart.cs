using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomrestart : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the camera
    public Transform target;  // The target object the camera is following
    public float minZoom = 5f;  // Minimum camera size (zoomed in)
    public float maxZoom = 10f;  // Maximum camera size (zoomed out)
    public float zoomSpeed = 1f;  // Speed at which the camera zooms in and out
    public float minDistance = 3f;  // The distance threshold to start zooming

    private void Update()
    {
        if (mainCamera.orthographic)
        {
            // Adjust camera zoom based on the distance to the target
            AdjustCameraZoom();
        }
    }

    private void AdjustCameraZoom()
    {
        float distance = Vector2.Distance(mainCamera.transform.position, target.position);

        if (distance < minDistance)
        {
            float zoomFactor = Mathf.InverseLerp(minDistance, 0f, distance);
            float targetSize = Mathf.Lerp(minZoom, maxZoom, zoomFactor);

            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
        }
        else
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, minZoom, zoomSpeed * Time.deltaTime);
        }
    }
}