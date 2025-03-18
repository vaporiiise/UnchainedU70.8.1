using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTriggerNH : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public int nextSceneIndex; // Index of the next scene to load
    public SpriteRenderer fadeSprite; // Assign a SpriteRenderer with a full-screen sprite
    public float fadeDuration = 1.5f; // Duration of the fade-in effect
    public float waitBeforeSceneChange = 1.0f; // Additional time to wait before changing scenes
    public Vector2 minArea; // Bottom-left corner of the detection area
    public Vector2 maxArea; // Top-right corner of the detection area
    public AudioClip ShutDoor;
    private AudioSource audioSource;

    private bool isPlayerInside = false; // Tracks if the player is inside the area

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckPlayerPosition();

        // If the player is inside the area and presses T, initiate the scene transition
        if (isPlayerInside && Input.GetKeyDown(KeyCode.T))
        {
            audioSource.PlayOneShot(ShutDoor);
            StartCoroutine(FadeAndChangeScene());
        }
    }

    private void CheckPlayerPosition()
    {
        if (player != null)
        {
            Vector2 playerPosition = player.transform.position;

            // Check if the player is inside the defined area
            isPlayerInside = playerPosition.x >= minArea.x && playerPosition.x <= maxArea.x &&
                             playerPosition.y >= minArea.y && playerPosition.y <= maxArea.y;
        }
    }

    IEnumerator FadeAndChangeScene()
    {
        if (fadeSprite != null)
        {
            float elapsedTime = 0f;
            Color spriteColor = fadeSprite.color;
            spriteColor.a = 0f; // Ensure the sprite starts fully transparent
            fadeSprite.color = spriteColor;

            // Gradually increase the alpha of the sprite
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                spriteColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                fadeSprite.color = spriteColor;
                yield return null;
            }
        }

        // Wait for an additional time after the fade
        yield return new WaitForSeconds(waitBeforeSceneChange);

        // Change to the next scene
        LoadNextScene();
    }

    void LoadNextScene()
    {
        if (nextSceneIndex >= 0 && nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogError("Invalid scene index!");
        }
    }

    // Visualize the area in the Scene view using Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 topLeft = new Vector3(minArea.x, maxArea.y, 0f);
        Vector3 bottomRight = new Vector3(maxArea.x, minArea.y, 0f);
        Vector3 topRight = new Vector3(maxArea.x, maxArea.y, 0f);
        Vector3 bottomLeft = new Vector3(minArea.x, minArea.y, 0f);

        // Draw the detection area as a rectangle
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
