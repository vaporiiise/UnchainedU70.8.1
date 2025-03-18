using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorUnlockEffect : MonoBehaviour
{
    [Header("References")]
    public Transform player; 
    public Transform objectToMove; 
    public float moveYDistance = 2f; 
    public GameObject fadeOutObject; 
    public float fadeDuration = 1f; 

    
    [Header("Effects")]
    public ParticleSystem effectPrefab; 
    public Transform effectSpawnPoint; 
    public AudioSource soundEffect; 

    [Header("Screen Shake Settings")]
    public float shakeDuration = 0.2f; 
    public float shakeIntensity = 0.1f;

    [Header("Trigger Settings")]
    public Vector2 triggerAreaCenter; 
    public float triggerRadius = 2f; 

    private bool effectTriggered = false;

    private void Update()
    {
        if (effectTriggered) return;

        if (Vector2.Distance(player.position, triggerAreaCenter) <= triggerRadius)
        {
            effectTriggered = true;
            StartEffect();
        }
    }

    void StartEffect()
    {
        objectToMove.position += new Vector3(0, moveYDistance, 0);

        StartCoroutine(FadeOutObject());

        if (soundEffect != null)
        {
            soundEffect.Play();
        }

        if (effectPrefab != null && effectSpawnPoint != null)
        {
            ParticleSystem newEffect = Instantiate(effectPrefab, effectSpawnPoint.position, Quaternion.identity);
            newEffect.Play();
            
            StartCoroutine(ScreenShake());
        }
    }

    IEnumerator FadeOutObject()
    {
        SpriteRenderer sr = fadeOutObject.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        float startAlpha = sr.color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            yield return null;
        }

        fadeOutObject.SetActive(false);
    }

    IEnumerator ScreenShake()
    {
        Camera mainCam = Camera.main; 
        if (mainCam == null) yield break; // Ensure we have a camera

        Vector3 originalPos = mainCam.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-shakeIntensity, shakeIntensity);
            float offsetY = Random.Range(-shakeIntensity, shakeIntensity);
            mainCam.transform.position = originalPos + new Vector3(offsetX, offsetY, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCam.transform.position = originalPos; 
    }

    private void OnDrawGizmos()
    {
        // Draw a circle in the Scene view to show the trigger area
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(triggerAreaCenter, triggerRadius);

        // Draw an arrow showing the object’s movement direction
        if (objectToMove != null)
        {
            Gizmos.color = Color.blue;
            Vector3 arrowStart = objectToMove.position;
            Vector3 arrowEnd = arrowStart + new Vector3(0, moveYDistance, 0);
            Gizmos.DrawLine(arrowStart, arrowEnd);
            Gizmos.DrawSphere(arrowEnd, 0.2f);
        }

        // Draw particle effect spawn position
        if (effectSpawnPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(effectSpawnPoint.position, 0.2f);
        }
    }
}