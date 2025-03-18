using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Image continueIcon; // 🔹 Smooth blinking icon

    [Header("Dialogue Settings")]
    [SerializeField] private List<string> dialogueLines;
    [SerializeField] private float textSpeed = 0.05f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip typingSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeOutDuration = 0.3f;

    [Header("Script to Disable")]
    [SerializeField] private Tilemovement scriptToDisable; 

    private int currentLineIndex = 0;
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private Coroutine fadeOutCoroutine;
    private Coroutine blinkCoroutine;

    private void Start()
    {
        dialogueBox.SetActive(false);
        if (continueIcon != null)
            continueIcon.gameObject.SetActive(false); // Hide icon initially

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
        }
    }

    private void Update()
    {
        if (isDialogueActive && !isTyping && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
        if (scriptToDisable != null)
        {
            scriptToDisable.enabled = !isDialogueActive;
        }
    }

    public void BeginDialogue()
    {
        if (dialogueLines.Count == 0) return;

        dialogueBox.SetActive(true);
        currentLineIndex = 0;
        isDialogueActive = true;
        StartTypingCurrentLine();
        BossMusicManager.Instance.OnDialogueStart();
    }

    private void StartTypingCurrentLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        if (continueIcon != null)
        {
            continueIcon.gameObject.SetActive(false); // Hide icon when new text starts
            continueIcon.color = new Color(continueIcon.color.r, continueIcon.color.g, continueIcon.color.b, 0f); // Fully transparent
        }

        typingCoroutine = StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        if (typingSound != null && audioSource != null)
        {
            StartCoroutine(PlayTypingSoundLoop());
        }

        foreach (char letter in line)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        StartCoroutine(FadeOutAudio());

        if (continueIcon != null)
        {
            continueIcon.gameObject.SetActive(true);
            if (blinkCoroutine != null) StopCoroutine(blinkCoroutine);
            blinkCoroutine = StartCoroutine(BlinkIcon());
        }
    }

    private IEnumerator BlinkIcon()
    {
        float duration = 0.6f; // Total fade cycle time
        float halfDuration = duration / 2;

        while (!isTyping) 
        {
            // Fade in
            for (float t = 0; t < halfDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(0f, 1f, t / halfDuration);
                continueIcon.color = new Color(continueIcon.color.r, continueIcon.color.g, continueIcon.color.b, alpha);
                yield return null;
            }

            // Fade out
            for (float t = 0; t < halfDuration; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(1f, 0f, t / halfDuration);
                continueIcon.color = new Color(continueIcon.color.r, continueIcon.color.g, continueIcon.color.b, alpha);
                yield return null;
            }
        }

        // Ensure it's fully visible at the end of dialogue
        continueIcon.color = new Color(continueIcon.color.r, continueIcon.color.g, continueIcon.color.b, 1f);
    }

    private IEnumerator PlayTypingSoundLoop()
    {
        while (isTyping)
        {
            if (typingSound != null && audioSource != null)
            {
                audioSource.clip = typingSound;
                audioSource.volume = 1f;
                audioSource.Play();
                yield return new WaitForSeconds(typingSound.length);
            }
        }
    }

    private IEnumerator FadeOutAudio()
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }

        fadeOutCoroutine = StartCoroutine(FadeOutCoroutine());
        yield return fadeOutCoroutine;
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeOutDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeOutDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }

    public void NextLine()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogueLines[currentLineIndex];
            isTyping = false;
            StartCoroutine(FadeOutAudio());

            if (continueIcon != null)
            {
                continueIcon.gameObject.SetActive(true);
                if (blinkCoroutine != null) StopCoroutine(blinkCoroutine);
                blinkCoroutine = StartCoroutine(BlinkIcon());
            }
            return;
        }

        if (continueIcon != null)
            continueIcon.gameObject.SetActive(false);

        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Count)
        {
            StartTypingCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        isDialogueActive = false;

        GameManager2.ResumeGame(); 

        StartCoroutine(FadeOutAudio());
        BossMusicManager.Instance.OnDialogueEnd();
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive;
    }
}