using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayButtonOnClick : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Assign the VideoPlayer component in the Inspector
    public Image fadeImage; // Assign the fade Image in the Inspector
    public CanvasGroup buttonCanvasGroup; // Assign the button's CanvasGroup component in the Inspector
    public float fadeDuration = 1.5f; // Duration of the fade
    public string nextSceneName; // Name of the next scene to load
    public GameObject toDisable;

    public Camera mainCamera; // Assign the main camera in the Inspector
    public float targetZPosition = -10f; // Desired Z position after movement
    public float cameraMoveDuration = 1.5f; // Duration of the camera movement
    public GameObject objectToMove; // Assign the GameObject to move in the Inspector
    public float targetXPosition = 5f; // Desired X position after movement
    public float objectMoveDuration = 1.5f; // Duration of the object movement
    private void Start()
    {
        fadeImage.color = new Color(0, 0, 0, 0); // Set fade image to transparent at start
        if (buttonCanvasGroup != null)
            buttonCanvasGroup.alpha = 1f; // Ensure button is fully visible at start
    }

    public void OnButtonClick()
    {
        toDisable.SetActive(false);
        MoveObjectOnX(); // Start object movement on the x-axis

        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd;

        if (buttonCanvasGroup != null)
            StartCoroutine(FadeOutButton());

        if (mainCamera != null)
            StartCoroutine(MoveCameraZPosition());
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        StartCoroutine(FadeToBlackAndLoadScene());
    }

    private IEnumerator FadeOutButton()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            buttonCanvasGroup.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            yield return null;
        }

        buttonCanvasGroup.alpha = 0f;
    }

    private IEnumerator MoveCameraZPosition()
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = mainCamera.transform.position;
        Vector3 targetPosition = new Vector3(startingPosition.x, startingPosition.y, targetZPosition);

        // Smoothly interpolate the camera position on the Z-axis
        while (elapsedTime < cameraMoveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / cameraMoveDuration;
            t = t * t * (3f - 2f * t);
            if (Input.anyKey)
            {
                SceneManager.LoadScene(nextSceneName);

            }
            mainCamera.transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            yield return null;
        }

        // Ensure camera reaches the target position
        mainCamera.transform.position = targetPosition;
    }

    private IEnumerator FadeToBlackAndLoadScene()
    {
        float elapsedTime = 0f;

        // Gradually increase alpha from 0 to 1
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Load the next scene after fade completes
        SceneManager.LoadScene(nextSceneName);
    }
    
    public void MoveObjectOnX()
    {
        if (objectToMove != null)
            StartCoroutine(MoveObjectXPosition());
    }

    private IEnumerator MoveObjectXPosition()
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = objectToMove.transform.position;
        Vector3 targetPosition = new Vector3(targetXPosition, startingPosition.y, startingPosition.z);

        while (elapsedTime < objectMoveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / objectMoveDuration;
            t = t * t * (3f - 2f * t); // Smoothstep interpolation for natural movement
            objectToMove.transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            yield return null;
        }

        objectToMove.transform.position = targetPosition;
    }

    
}