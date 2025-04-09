using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerAreaWithObjMove : MonoBehaviour
{
    public Image targetImage;          
    public RectTransform targetRect;  
    public TMP_Text targetText;       
    public Vector3 newPosition;       
    public float fadeDuration = 1f;   
    public float triggerDuration = 5f; 
    public float displayDuration = 60f; 
    public Vector3 areaMin;           
    public Vector3 areaMax;           
    public Color gizmoColor = new Color(0f, 1f, 0f, 0.25f); 

    public AudioClip popUpSound; // 🎵 Sound effect for UI pop-up
    private AudioSource audioSource;  

    private Vector3 originalPosition; 
    private bool playerInArea = false;
    private float timeInArea = 0f;
    private Coroutine fadeCoroutine;

    void Start()
    {
        originalPosition = targetRect.anchoredPosition;
        SetAlpha(0f);

        // 🎵 Add an AudioSource component if not present
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        Vector3 playerPosition = PlayerPosition(); 
        bool isInArea = IsPlayerInArea(playerPosition);

        if (isInArea)
        {
            playerInArea = true;
            timeInArea += Time.deltaTime;

            if (timeInArea >= triggerDuration && fadeCoroutine == null)
            {
                fadeCoroutine = StartCoroutine(FadeInAndMove());
            }
        }
        else
        {
            if (playerInArea)
            {
                playerInArea = false;
                timeInArea = 0f;

                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(FadeOutAndReset());
            }
        }
    }

    private bool IsPlayerInArea(Vector3 position)
    {
        return position.x >= areaMin.x && position.x <= areaMax.x &&
               position.y >= areaMin.y && position.y <= areaMax.y &&
               position.z >= areaMin.z && position.z <= areaMax.z;
    }

    private Vector3 PlayerPosition()
    {
        return GameObject.FindWithTag("Player").transform.position;
    }

    private IEnumerator FadeInAndMove()
    {
        // 🎵 Play pop-up sound if assigned
        if (popUpSound != null && audioSource != null)
        {
            SFXManager.instance.PlaySFX(popUpSound);
            //audioSource.PlayOneShot(popUpSound);
        }

        yield return StartCoroutine(FadeAlpha(1f, fadeDuration));
        targetRect.anchoredPosition = newPosition;

        yield return new WaitForSeconds(displayDuration);

        fadeCoroutine = StartCoroutine(FadeOutAndReset());
    }

    private IEnumerator FadeOutAndReset()
    {
        yield return StartCoroutine(FadeAlpha(0f, fadeDuration));
        targetRect.anchoredPosition = originalPosition;
        fadeCoroutine = null;
    }

    private IEnumerator FadeAlpha(float targetAlpha, float duration)
    {
        float startAlpha = targetImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(targetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        Color imageColor = targetImage.color;
        imageColor.a = alpha;
        targetImage.color = imageColor;

        if (targetText != null)
        {
            Color textColor = targetText.color;
            textColor.a = alpha;
            targetText.color = textColor;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube((areaMin + areaMax) / 2, areaMax - areaMin);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((areaMin + areaMax) / 2, areaMax - areaMin);
    }
}