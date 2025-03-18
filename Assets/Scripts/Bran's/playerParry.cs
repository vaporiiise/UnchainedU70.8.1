using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class playerParry : MonoBehaviour
{
    public float parryDuration = 0.5F;
    public float parryCooldown = 3F;
    public List<AudioClip> parryAudioClips; 
    private AudioSource audioSource; 

    private bool isParrying = false;
    private bool canParry = true;
    private bool parrySuccess = false;

    public GameObject parryHitbox;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && canParry)
            StartCoroutine(ParryWindow());
    }

    IEnumerator ParryWindow()
    {
        parrySuccess = false;
        canParry = false;
        isParrying = true;

        Debug.Log("Parry Window Up!");
        parryHitbox.SetActive(true);
        parryHitbox.transform.position = (Vector2)transform.position * 1.0F;
        yield return new WaitForSeconds(parryDuration);
        parryHitbox.SetActive(false);
        Debug.Log("Parry Window Down!");
        isParrying = false;

        if (!parrySuccess)
            yield return new WaitForSeconds(parryCooldown);

        canParry = true;
    }

    private void OnTriggerEnter2D(Collider2D parryCol)
    {
       if (isParrying && parryCol.CompareTag("BossAttack"))
        {
            Debug.Log("Parried!");
            parrySuccess = true;
            bossAI boss = parryCol.GetComponentInParent<bossAI>();
            playerAttack player = parryCol.GetComponentInParent<playerAttack>();
            boss.TakeDamage(50);

            PlayRandomAudio();
        }
    }
    public void PlayRandomAudio()
    {
        if (parryAudioClips.Count > 0)
        {
            // Pick a random audio clip from the list
            int randomIndex = Random.Range(0, parryAudioClips.Count);
            AudioClip selectedClip = parryAudioClips[randomIndex];

            // Play the selected audio clip
            audioSource.PlayOneShot(selectedClip);
        }
        else
        {
            Debug.LogWarning("No audio clips assigned to the list!");
        }
    }
}