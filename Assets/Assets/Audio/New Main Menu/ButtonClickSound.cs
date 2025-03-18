using UnityEngine;
using UnityEngine.UI;

public class SpriteButtonClickSound : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource to play the sound
    public AudioClip clickSound; // Click sound effect

    private void Start()
    {
        // Get the Button component attached to this GameObject
        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(PlayClickSound);
        }
        else
        {
            Debug.LogWarning("Button component is missing on this GameObject!", this);
        }
    }

    private void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
            Debug.Log("Click sound played!");
        }
        else
        {
            Debug.LogWarning("AudioSource or ClickSound is not assigned!", this);
        }
    }
}
