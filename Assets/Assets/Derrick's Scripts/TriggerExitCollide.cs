using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerExitCollide : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public int nextSceneIndex; // Index of the next scene to load
    public SpriteRenderer fadeSprite; // Assign a SpriteRenderer with a full-screen sprite
    public float fadeDuration = 1.5f; // Duration of the fade-in effect
    public float waitBeforeSceneChange = 1.0f; // Additional time to wait before changing scenes
    public AudioClip ShutDoor;
    private AudioSource audioSource;

    private bool isPlayerInside = false; // Tracks if the player is inside the trigger area
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the trigger
        if (other.gameObject == player)
        {
            isPlayerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exited the trigger
        if (other.gameObject == player)
        {
            isPlayerInside = false;
        }
    }

    void Update()
    {
        // If the player is inside the trigger and presses T, initiate the scene transition
        if (isPlayerInside && Input.GetKeyDown(KeyCode.T))
        {
            audioSource.PlayOneShot(ShutDoor);

            StartCoroutine(FadeAndChangeScene());
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
}
