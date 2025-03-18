using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    [Header("Jump Attack Settings")]
    [SerializeField] private float jumpAttackDuration = 0.3f;
    [SerializeField] private float jumpAttackMagnitude = 0.2f;

    [Header("Ground Slam Settings")]
    [SerializeField] private float groundSlamDuration = 0.2f;
    [SerializeField] private float groundSlamMagnitude = 0.04f;

    [Header("Hand Slam Settings")]
    [SerializeField] private float handSlamDuration = 0f;
    [SerializeField] private float handSlamMagnitude = 0f;

    [Header("UI Shake Settings")]
    [SerializeField] private RectTransform playerUIElementToShake;
    [SerializeField] private RectTransform bossUIElementToShake;
    [SerializeField] private RectTransform playerDamageUIElementToShake;
    [SerializeField] private RectTransform bossDamageUIElementToShake;
    [SerializeField] private float uiShakeDuration = 0.2f;
    [SerializeField] private float uiShakeMagnitude = 0.2f;

    private Vector3 shakeOffset;
    private FollowCamera followCameraScript;
    private bossAI bossScript;
    private playerAttack playerScript;

    private bool isPlayerUIShaking = false;
    private bool isBossUIShaking = false;
    private bool isPlayerDamageUIShaking = false;
    private bool isBossDamageUIShaking = false;

    private int lastPlayerHealth = 0;
    private int lastBossHealth = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        bossScript = FindObjectOfType<bossAI>();
        followCameraScript = Camera.main.GetComponent<FollowCamera>();
        playerScript = FindObjectOfType<playerAttack>();

        if (playerScript != null)
            lastPlayerHealth = playerScript.currentHealth;

        if (bossScript != null)
            lastBossHealth = bossScript.currentHealth;
    }

    private void OnEnable()
    {
        shakeOffset = Vector3.zero;
        isPlayerUIShaking = false;
        isBossUIShaking = false;
        isPlayerDamageUIShaking = false;
        isBossDamageUIShaking = false;
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            shakeOffset = new Vector3(offsetX, offsetY, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        shakeOffset = Vector3.zero;
    }

    private void Update()
    {
        // Handle the boss's attack shake patterns
        if (bossScript != null)
        {
            if (bossScript.attackPattern == 3 && bossScript.handSwipeHitBox.activeInHierarchy)
            {
                Shake(jumpAttackDuration, jumpAttackMagnitude);
            }
            else if (bossScript.attackPattern == 2 && bossScript.groundSlamHitBox.activeInHierarchy)
            {
                Shake(groundSlamDuration, groundSlamMagnitude);
            }
            else if (bossScript.attackPattern == 1 && bossScript.handSlamHitBox.activeInHierarchy)
            {
                Shake(handSlamDuration, handSlamMagnitude);
            }
        }

        // Trigger UI shake when the player's health decreases
        if (playerScript != null && playerScript.currentHealth < lastPlayerHealth)
        {
            if (!isPlayerUIShaking)
            {
                StartCoroutine(UIShakeCoroutine(playerUIElementToShake, () => isPlayerUIShaking = false));
                isPlayerUIShaking = true;
            }

            if (!isPlayerDamageUIShaking)
            {
                StartCoroutine(UIShakeCoroutine(playerDamageUIElementToShake, () => isPlayerDamageUIShaking = false));
                isPlayerDamageUIShaking = true;
            }
        }

        // Trigger UI shake when the boss's health decreases
        if (bossScript != null && bossScript.currentHealth < lastBossHealth)
        {
            if (!isBossUIShaking)
            {
                StartCoroutine(UIShakeCoroutine(bossUIElementToShake, () => isBossUIShaking = false));
                isBossUIShaking = true;
            }

            if (!isBossDamageUIShaking)
            {
                StartCoroutine(UIShakeCoroutine(bossDamageUIElementToShake, () => isBossDamageUIShaking = false));
                isBossDamageUIShaking = true;
            }
        }

        // Update health values for the next frame
        if (playerScript != null)
            lastPlayerHealth = playerScript.currentHealth;

        if (bossScript != null)
            lastBossHealth = bossScript.currentHealth;

        // Apply screen shake offset to the camera
        if (shakeOffset != Vector3.zero && followCameraScript != null)
        {
            followCameraScript.transform.position += shakeOffset;
        }
    }

    private IEnumerator UIShakeCoroutine(RectTransform uiElement, System.Action onComplete)
    {
        if (uiElement == null)
        {
            Debug.LogError("UI Element to shake is null!");
            onComplete?.Invoke();
            yield break;
        }

        Vector3 originalPosition = uiElement.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < uiShakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * uiShakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * uiShakeMagnitude;

            uiElement.anchoredPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiElement.anchoredPosition = originalPosition;
        onComplete?.Invoke();
    }
}
