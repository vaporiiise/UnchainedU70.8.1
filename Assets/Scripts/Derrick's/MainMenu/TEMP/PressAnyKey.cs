using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressAnyKey : MonoBehaviour
{
    public Image fadeScreen;
    public Image fadeImage1;
    public Image fadeImage2;
    public Animator imageAnimator2;
    public AudioSource audioSource;
    public Canvas targetCanvas;

    public AudioClip initialClip; // First sound effect
    public AudioClip loopingClip; // Continuous background loop

    private bool isFading = false;
    private bool canActivateCanvas = false;
    private bool inputDisabled = false;
    private float timeSinceFirstKey = 0f;

    void Start()
    {
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.color = new Color(0, 0, 0, 1);
        StartCoroutine(FadeInScreen());
    }

    void Update()
    {
        if (!isFading && !inputDisabled && Input.anyKeyDown)
        {
            isFading = true;
            inputDisabled = true; // Disable input after key press
            StartCoroutine(StartGameSequence());
        }

        if (canActivateCanvas && timeSinceFirstKey >= 10f)
        {
            targetCanvas.gameObject.SetActive(true);
            canActivateCanvas = false;
        }
    }

    IEnumerator FadeInScreen()
    {
        float fadeDuration = 3f;
        float frameTime = 1f / 10f;
        float fadeStep = frameTime / fadeDuration;
        Color color = fadeScreen.color;

        for (float t = 0; t < 1; t += fadeStep)
        {
            color.a = Mathf.Lerp(1, 0, t);
            fadeScreen.color = color;
            yield return new WaitForSeconds(frameTime);
        }

        color.a = 0;
        fadeScreen.color = color;
        fadeScreen.gameObject.SetActive(false);
    }

    IEnumerator StartGameSequence()
    {
        // Play initial sound without waiting
        if (audioSource && initialClip)
        {
            audioSource.PlayOneShot(initialClip);
            StartCoroutine(PlayLoopingMusicAfterDelay(initialClip.length)); // Start looping music after it finishes
        }

        // Immediately start animations and fade effect
        if (imageAnimator2) 
        {
            imageAnimator2.SetTrigger("PlayEffect");
        }

        float fadeDuration = 3f;
        float frameTime = 1f / 10f;
        float fadeStep = frameTime / fadeDuration;

        Color color1 = fadeImage1.color;
        Color color2 = fadeImage2.color;

        for (float t = 0; t < 1; t += fadeStep)
        {
            color1.a = Mathf.Lerp(1, 0, t);
            color2.a = Mathf.Lerp(1, 0, t);
            fadeImage1.color = color1;
            fadeImage2.color = color2;
            yield return new WaitForSeconds(frameTime);
        }

        color1.a = 0;
        color2.a = 0;
        fadeImage1.color = color1;
        fadeImage2.color = color2;

        fadeImage1.gameObject.SetActive(false);
        fadeImage2.gameObject.SetActive(false);

        yield return new WaitForSeconds(10f);
        canActivateCanvas = true;
        timeSinceFirstKey = 10f;
        
        inputDisabled = false; // Re-enable input after fade-out
        
    }

    IEnumerator PlayLoopingMusicAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait until the initialClip finishes

        // Start looping background sound
        if (audioSource && loopingClip)
        {
            audioSource.clip = loopingClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}