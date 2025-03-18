using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f);  // Scale to enlarge on hover
    public float animationDuration = 0.2f;                      // Duration for scale animation
    public AudioClip hoverSound;                                // Hover sound effect
    public AudioClip clickSound;                                // Click sound effect

    private Vector3 originalScale;
    private bool isHovering = false;
    private AudioSource audioSource;
    private Button button;

    
    private void Start()
    {
        originalScale = transform.localScale;  // Store the original scale of the button
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();

        // Ensure there's an AudioSource
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;  // Prevents sound from playing on start

        // Add the OnClick listener to play click sound
        if (button != null)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover sound and enlarge button when the pointer enters
        if (!isHovering && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
        StopAllCoroutines();
        StartCoroutine(ScaleButton(hoverScale));
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Return button to original size when the pointer exits
        StopAllCoroutines();
        StartCoroutine(ScaleButton(originalScale));
        isHovering = false;
    }

    private IEnumerator ScaleButton(Vector3 targetScale)
    {
        float elapsedTime = 0f;
        Vector3 startingScale = transform.localScale;

        while (elapsedTime < animationDuration)
        {
            transform.localScale = Vector3.Lerp(startingScale, targetScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
