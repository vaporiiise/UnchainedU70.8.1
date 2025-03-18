using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDamagePlayer : MonoBehaviour
{
    [Header("Screen Shake Settings")]
    public float shakeDuration = 0.2f; 
    public float shakeIntensity = 0.1f;

    [Header("Damage Settings")] 
    public int damage = 20;
    public AudioSource soundEffect;
    public GameObject damageIndicator;
    public float damageDisplayDuration = 0.5f;
    
    BHPlayerHeaalth playerHealth;




    private void Start()
    {
        if (damageIndicator != null)
        {
            damageIndicator.gameObject.SetActive(false);  // Ensure damage indicator is off at the start
        }
    }

    public void PlayerIsBeamed()
    {
            playerHealth.TakeDamage(damage);
            StartCoroutine(ScreenShake());
            PlaySound();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Get the NHPlayerHealth component from the Player GameObject
            playerHealth = other.GetComponent<BHPlayerHeaalth>();

            if (playerHealth != null)
            {
                PlayerIsBeamed();
            }
            else
            {
                Debug.LogError("NHPlayerHealth script not found on Player GameObject!");
            }
        }
    }

    IEnumerator ScreenShake()
    {
        Camera mainCam = Camera.main; 
        if (mainCam == null) yield break; 

        Vector3 originalPos = mainCam.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float offsetX = UnityEngine.Random.Range(-shakeIntensity, shakeIntensity);
            float offsetY = UnityEngine.Random.Range(-shakeIntensity, shakeIntensity);
            mainCam.transform.position = originalPos + new Vector3(offsetX, offsetY, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCam.transform.position = originalPos; 
    }

    private void PlaySound()
    {
        soundEffect.Play();
    }
    
    IEnumerator ShowDamageIndicator()
    {
        damageIndicator.gameObject.SetActive(true); 
        yield return new WaitForSeconds(damageDisplayDuration); 
        damageIndicator.gameObject.SetActive(false); 
    }
}
