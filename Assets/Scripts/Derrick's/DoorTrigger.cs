using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform box1;
    public Transform box2;
    public Transform targetPosition1;
    public Transform targetPosition2;
    public GameObject door;
    public SpriteRenderer lockSprite;
    public float threshold = 0.5f;
    public AudioClip doorUnlockSound;
    public MonoBehaviour scriptToDisable;
    public Transform moveToPosition;
    public float moveSpeed = 20f;
    public Camera mainCamera;
    public Transform cameraTarget;
    public float cameraPanSpeed = 5f;
    public float shakeDuration = 0.2f;
    public float shakeIntensity = 0.2f;
    public float spriteFadeSpeed = 1.5f;
    public ParticleSystem unlockEffect; 
    public float particleDuration = 1.5f; 

    private bool doorOpened = false;

    void Start()
    {
        if (lockSprite != null)
        {
            Color c = lockSprite.color;
            c.a = 1; 
            lockSprite.color = c;
        }
    }

    void Update()
    {
        if (Vector2.Distance(box1.position, targetPosition1.position) < threshold &&
            Vector2.Distance(box2.position, targetPosition2.position) < threshold && !doorOpened)
        {
            doorOpened = true;
            StartCoroutine(OpenDoorSequence());
        }
    }

    IEnumerator OpenDoorSequence()
    {
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = false;
        }

        yield return StartCoroutine(PanCamera());
        
        if (doorUnlockSound != null)
        {
            SFXManager.instance.PlaySFX(doorUnlockSound);
            //AudioSource.PlayClipAtPoint(doorUnlockSound, Camera.main.transform.position, 1.0f);
        }

        if (unlockEffect != null)
        {
            unlockEffect.Play();
        }

        StartCoroutine(CameraShake());

        if (lockSprite != null)
        {
            StartCoroutine(FadeOutSprite());
        }

        StartCoroutine(MoveDoor());

        yield return new WaitForSeconds(particleDuration);

        if (unlockEffect != null)
        {
            unlockEffect.Stop();
        }

        yield return new WaitForSeconds(0.5f);

        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = true;
        }
    }

    IEnumerator MoveDoor()
    {
        float startTime = Time.time;
        Vector3 startPosition = door.transform.position;
        Vector3 endPosition = moveToPosition.position;
        float journeyLength = Vector3.Distance(startPosition, endPosition);

        while (Vector3.Distance(door.transform.position, endPosition) > 0.05f)
        {
            float elapsedTime = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = elapsedTime / journeyLength;
            door.transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            yield return null;
        }

        door.transform.position = endPosition; 
    }

    IEnumerator CameraShake()
    {
        Vector3 originalPos = mainCamera.transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-shakeIntensity, shakeIntensity);
            float y = Random.Range(-shakeIntensity, shakeIntensity);
            mainCamera.transform.position = originalPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPos; 
    }

    IEnumerator FadeOutSprite()
    {
        Color c = lockSprite.color;
        float alpha = 1f;
        float startTime = Time.time;

        while (alpha > 0f) 
        {
            alpha = Mathf.Clamp01(1f - (Time.time - startTime) * spriteFadeSpeed);
            c.a = alpha;
            lockSprite.color = c;
            yield return null;
        }

        c.a = 0;
        lockSprite.color = c; 
    }

    IEnumerator PanCamera()
    {
        Vector3 startCamPos = mainCamera.transform.position;
        Vector3 endCamPos = cameraTarget.position;
        float startTime = Time.time;
        float duration = 0.5f; 

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            mainCamera.transform.position = Vector3.Lerp(startCamPos, endCamPos, t);
            yield return null;
        }

        mainCamera.transform.position = endCamPos;

        yield return new WaitForSeconds(0.5f); 
    }

    
}