using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveOnClick : MonoBehaviour
{
    public float cameraMoveDuration = 1.5f;
    public Vector3 cameraTargetPos;
    public float smoothTime = 0.2f;

    private void OnButtonClick()
    {
        StartCoroutine(MoveCameraToTarget());

    }
    private IEnumerator MoveCameraToTarget()
    {
        Camera mainCamera = Camera.main;
        Vector3 velocity = Vector3.zero; // Required by SmoothDamp to smoothly reach the target position
        float elapsedTime = 0f;

        // Smoothly move the camera to the target position using SmoothDamp
        while (elapsedTime < cameraMoveDuration)
        {
            mainCamera.transform.position =
                Vector3.SmoothDamp(mainCamera.transform.position, cameraTargetPos, ref velocity, smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the camera reaches the exact target position
        mainCamera.transform.position = cameraTargetPos;
      
      
    }
}
