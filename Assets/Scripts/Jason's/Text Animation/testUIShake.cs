using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testUIShake : MonoBehaviour
{
    // This is a manual UI shake for testing purposes (currently not in use)
    public RectTransform testUIElement;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(ShakeUI());
        }
    }

    IEnumerator ShakeUI()
    {
        Vector3 originalPosition = testUIElement.anchoredPosition;
        float duration = 0.5f;
        float magnitude = 10f;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            testUIElement.anchoredPosition = originalPosition + new Vector3(offsetX, offsetY, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        testUIElement.anchoredPosition = originalPosition;
    }
}
