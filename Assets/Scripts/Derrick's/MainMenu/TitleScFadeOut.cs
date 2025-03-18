using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleScFadeOut : MonoBehaviour
{
    public RawImage rawImage;                 
    public TextMeshProUGUI uiText;            
    public float fadeInDuration = 1f;         
    public float fadeOutDuration = 1f;        
    public AudioSource audioSource;           
    public AudioClip clickSound;              
    public Vector3 cameraTargetPosition;      
    public float cameraMoveDuration = 2f;     
    public float smoothTime = 0.3f;           

    public RectTransform buttonToMove;        
    public float buttonTargetXPos;            
    public float buttonSmoothTime = 0.2f;     

    public RectTransform secondButton;        
    public float secondButtonTargetXPos;      
    public float secondButtonSmoothTime = 0.2f; 

    public RectTransform thirdButton;         
    public float thirdButtonTargetXPos;       
    public float thirdButtonSmoothTime = 0.2f; 

    public RectTransform fourthButton;        
    public float fourthButtonTargetXPos;      
    public float fourthButtonSmoothTime = 0.2f; 

    public RectTransform fifthButton;         
    public float fifthButtonTargetXPos;       
    public float fifthButtonSmoothTime = 0.2f; 
    public float delayBetweenButtonMoves = 0.2f; 

    private bool isFadingOut = false;         
    private bool fadeInComplete = false;      

    private void Start()
    {
        StartCoroutine(FadeInText());
    }

    private void Update()
    {
        // Wait for a mouse click to trigger the effect
        if (fadeInComplete && Input.GetMouseButtonDown(0) && !isFadingOut)
        {
            StartCoroutine(FlashThenFadeOut());
            isFadingOut = true;
            audioSource?.PlayOneShot(clickSound);
        }
    }

    private IEnumerator FadeInText()
    {
        float elapsedTime = 0f;
        Color textColor = uiText.color;
        textColor.a = 0f;
        uiText.color = textColor;

        // Fade in the text
        while (elapsedTime < fadeInDuration)
        {
            textColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            uiText.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textColor.a = 1f;
        uiText.color = textColor;
        fadeInComplete = true; // Now we can detect clicks
    }

    private IEnumerator FlashThenFadeOut()
    {
        // Flashing effect
        for (int i = 0; i < 5; i++) // Flash 5 times
        {
            uiText.enabled = !uiText.enabled;
            yield return new WaitForSeconds(0.15f); // Faster flashing
        }
        uiText.enabled = true;

        // Fade out everything
        float elapsedTime = 0f;
        Color imageColor = rawImage.color;
        Color textColor = uiText.color;

        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);

            imageColor.a = alpha;
            rawImage.color = imageColor;

            textColor.a = alpha;
            uiText.color = textColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rawImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f);
        uiText.color = new Color(textColor.r, textColor.g, textColor.b, 0f);

        StartCoroutine(MoveCameraToTarget());
    }

    private IEnumerator MoveCameraToTarget()
    {
        Camera mainCamera = Camera.main;
        Vector3 velocity = Vector3.zero;
        float elapsedTime = 0f;

        while (elapsedTime < cameraMoveDuration)
        {
            mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, cameraTargetPosition, ref velocity, smoothTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = cameraTargetPosition;

        yield return MoveButtonToTarget(buttonToMove, buttonTargetXPos, buttonSmoothTime);
        yield return MoveButtonToTarget(secondButton, secondButtonTargetXPos, secondButtonSmoothTime);
        yield return MoveButtonToTarget(thirdButton, thirdButtonTargetXPos, thirdButtonSmoothTime);
        yield return MoveButtonToTarget(fourthButton, fourthButtonTargetXPos, fourthButtonSmoothTime);
        yield return MoveButtonToTarget(fifthButton, fifthButtonTargetXPos, fifthButtonSmoothTime);
    }

    private IEnumerator MoveButtonToTarget(RectTransform button, float targetXPos, float smoothTime)
    {
        Vector3 velocity = Vector3.zero;
        Vector3 startPos = button.anchoredPosition;
        Vector3 targetPos = new Vector3(targetXPos, startPos.y, startPos.z);

        while (Vector3.Distance(button.anchoredPosition, targetPos) > 0.1f)
        {
            button.anchoredPosition = Vector3.SmoothDamp(button.anchoredPosition, targetPos, ref velocity, smoothTime);
            yield return null;
        }

        button.anchoredPosition = targetPos;
    }
}