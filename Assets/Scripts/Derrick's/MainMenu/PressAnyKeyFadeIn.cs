using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PressAnyKeyFadeIn : MonoBehaviour
{
    public TextMeshProUGUI uiText;     // Reference to the TextMeshProUGUI component
    public float fadeDuration = 1f;    // Duration of the fade-in

    private void Start()
    {
        Color textColor = uiText.color;
        textColor.a = 0f;              // Set initial alpha to 0 (fully transparent)
        uiText.color = textColor;
        
        StartCoroutine(FadeInText());
    }

    private IEnumerator FadeInText()
    {
        yield return new WaitForSeconds(3f);  // Wait for 3 seconds

        float elapsedTime = 0f;
        Color textColor = uiText.color;

        while (elapsedTime < fadeDuration)
        {
            textColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            uiText.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textColor.a = 1f;              // Ensure the final alpha is set to 1 (fully opaque)
        uiText.color = textColor;
    }
}
