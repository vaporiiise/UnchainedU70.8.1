using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossDoorTrigger : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public List<Transform> boxes = new List<Transform>();
    public List<Transform> targetPositions = new List<Transform>();
    public float threshold = 0.5f;

    [Header("Door Settings")]
    public GameObject door;
    public SpriteRenderer lockSprite;
    public Transform moveToPosition;
    public float moveSpeed = 20f;

    [Header("Effects & Audio")]
    public AudioClip doorUnlockSound;
    public ParticleSystem unlockEffect;
    public float particleDuration = 1.5f;
    public AudioClip tickSound;

    [Header("Camera Settings")]
    public Camera mainCamera;
    public Transform cameraTarget;
    public float shakeDuration = 0.2f;
    public float shakeIntensity = 0.2f;
    public float spriteFadeSpeed = 1.5f;

    [Header("Dialogue & Timer")]
    public DialogueSystem dialogueSystem;
    public TMP_Text timerText;
    public float timeLimit = 10f;
    private float timer;
    private bool timerRunning = false;
    private bool timeExpired = false;
    private bool doorOpened = false;
    public RectTransform timerBox;

    [Header("Other Settings")]
    public MonoBehaviour scriptToDisable;
    public Transform playerTriggerZone;
    private bool triggerActivated = false;
    public int damage = 25;
    private bossHealth bossHealth;
    public List<GameObject> staticCharge;
    
    [Header("Game Object Activation")]
    public GameObject objectToEnable;

    private void Start()
    {
        bossHealth = FindObjectOfType<bossHealth>();
        triggerActivated = false; // Ensures it's always reset when the scene reloads
        timerText.gameObject.SetActive(false);
        timer = timeLimit;
    
        if (lockSprite != null)
        {
            Color c = lockSprite.color;
            c.a = 1;
            lockSprite.color = c;
        }
    }

    private void Update()
    {
        if (timerRunning)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timerText.text = $"{timer:F2}";
            }
            else if (!timeExpired)
            {
                timeExpired = true;
                timer = 0;
                timerText.text = "00.00";
                Debug.Log("Player is SLOW!");
                foreach (GameObject obj in staticCharge)
                {
                    obj.SetActive(false);
                }
            }

            ShakeAndRotateTimer();
        }

        if (!doorOpened && AreAllBoxesInPosition())
        {
            if (!timeExpired) 
            {
                foreach (GameObject obj in staticCharge)
                {
                    obj.SetActive(false);
                }
                StopTimer();
                Debug.Log("Player wins!");
                if (bossHealth != null) bossHealth.TakeDamage(damage);
            }
            else
            {
                Debug.Log("Player is SLOW!");
                
            }

            doorOpened = true;
            StartCoroutine(OpenDoorSequence());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerActivated || !other.CompareTag("Player"))
            return;
    
        StartTimer();  // Start the timer first
        triggerActivated = true; // Now prevent re-triggering
    }

    private void StartTimer()
    {
        timerText.gameObject.SetActive(true);
        timerRunning = true;
        StartCoroutine(PlayTickSound());
    }

    private void StopTimer()
    {
        timerRunning = false;
        StopAllCoroutines(); // Stops ticking sound
        timerText.gameObject.SetActive(false); // Hide timer UI
    }

    private IEnumerator PlayTickSound()
    {
        while (timerRunning && timer > 0)
        {
            if (tickSound != null)
                AudioSource.PlayClipAtPoint(tickSound, transform.position, 1.0f);
            yield return new WaitForSeconds(1f);
        }
    }

    private void ShakeAndRotateTimer()
    {
        float shakeAmount = 0.3f;
        float rotationAmount = 0.4f;

        Vector3 minBounds = timerBox.position - (Vector3)timerBox.rect.size / 2;
        Vector3 maxBounds = timerBox.position + (Vector3)timerBox.rect.size / 2;

        Vector3 newPos = timerText.transform.position + (Vector3)Random.insideUnitCircle * shakeAmount;
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y, maxBounds.y);

        timerText.transform.position = newPos;
        timerText.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-rotationAmount, rotationAmount));
    }

    private bool AreAllBoxesInPosition()
    {
        if (boxes.Count != targetPositions.Count)
        {
            Debug.LogWarning("Mismatch between the number of boxes and target positions!");
            return false;
        }

        for (int i = 0; i < boxes.Count; i++)
        {
            if (Vector2.Distance(boxes[i].position, targetPositions[i].position) >= threshold)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator OpenDoorSequence()
    {
        if (scriptToDisable != null)
            scriptToDisable.enabled = false;

        yield return StartCoroutine(PanCamera());

        if (doorUnlockSound != null)
            AudioSource.PlayClipAtPoint(doorUnlockSound, Camera.main.transform.position, 1.0f);

        if (unlockEffect != null)
            unlockEffect.Play();

        StartCoroutine(CameraShake());
        StartDialogue();

        if (lockSprite != null)
            StartCoroutine(FadeOutSprite());

        StartCoroutine(MoveDoor());

        yield return new WaitForSeconds(particleDuration);

        if (unlockEffect != null)
            unlockEffect.Stop();

        yield return new WaitForSeconds(0.5f);

        if (scriptToDisable != null)
            scriptToDisable.enabled = true;

        timerText.gameObject.SetActive(false);

    }

    private IEnumerator MoveDoor()
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

    private IEnumerator CameraShake()
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

    private IEnumerator FadeOutSprite()
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

    private IEnumerator PanCamera()
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

    private void StartDialogue()
    {
        if (dialogueSystem != null)
        {
            dialogueSystem.BeginDialogue();
            StartCoroutine(WaitForDialogueToEnd());
        }
    }

    private IEnumerator WaitForDialogueToEnd()
    {
        while (dialogueSystem.IsDialogueActive()) // Ensure this method exists in DialogueSystem
        {
            yield return null; // Wait until dialogue ends
        }

        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
    }
}