using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivateBossFight : MonoBehaviour
{
    [Header("Dialogue System")]
    public DialogueSystem dialogueSystem;

    [Header("GameObject Activation")]
    public GameObject objectToActivate;

    [Header("Text Display Effect")]
    public TMP_Text effectText; // Assign a TMP Text in the inspector
    public float fadeDuration = 1.5f;
    public float scaleDecreaseSpeed = 0.5f;
    public float effectDuration = 2f; // Total time the text is visible

    [Header("Script Activation")]
    public MonoBehaviour scriptToActivate;

    public void StartSequence()
    {
        StartCoroutine(SequenceRoutine());
    }

    private IEnumerator SequenceRoutine()
    {
        // Step 1: Trigger dialogue
        if (dialogueSystem != null)
        {
            dialogueSystem.BeginDialogue();
        }

        // Step 2: Wait 5 seconds
        yield return new WaitForSeconds(5f);

        // Step 3: Activate GameObject
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        // Step 4: Display and animate text effect
        if (effectText != null)
        {
            yield return StartCoroutine(ShowTextEffect());
        }

        // Step 5: Enable the specified script
        if (scriptToActivate != null)
        {
            scriptToActivate.enabled = true;
        }
    }

    private IEnumerator ShowTextEffect()
    {
        effectText.gameObject.SetActive(true);
        effectText.color = new Color(effectText.color.r, effectText.color.g, effectText.color.b, 0);
        effectText.transform.localScale = Vector3.one;

        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            effectText.color = new Color(effectText.color.r, effectText.color.g, effectText.color.b, alpha);
            yield return null;
        }

        // Hold for effectDuration
        yield return new WaitForSeconds(effectDuration);

        // Decrease scale & fade out
        elapsedTime = 0f;
        Vector3 originalScale = effectText.transform.localScale;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1 - (elapsedTime / fadeDuration);
            effectText.color = new Color(effectText.color.r, effectText.color.g, effectText.color.b, alpha);
            effectText.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / fadeDuration);
            yield return null;
        }

        // Hide text after animation
        effectText.gameObject.SetActive(false);
    }
}
