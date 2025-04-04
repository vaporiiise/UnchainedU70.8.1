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
    public TMP_Text effectText; 
    public float fadeDuration = 1.5f;
    public float scaleDecreaseSpeed = 0.5f;
    public float effectDuration = 2f; 

    [Header("Script Activation")]
    public MonoBehaviour scriptToActivate;

    public void StartSequence()
    {
        StartCoroutine(SequenceRoutine());
    }

    private IEnumerator SequenceRoutine()
    {
        if (dialogueSystem != null)
        {
            dialogueSystem.BeginDialogue();
        }

        yield return new WaitForSeconds(5f);

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        if (effectText != null)
        {
            yield return StartCoroutine(ShowTextEffect());
        }

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

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            effectText.color = new Color(effectText.color.r, effectText.color.g, effectText.color.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(effectDuration);

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

        effectText.gameObject.SetActive(false);
    }
}
