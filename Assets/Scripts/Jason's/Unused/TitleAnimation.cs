using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleAnimation : MonoBehaviour
{
    public TMP_Text textComponent;

    [Header("Shiver Settings")]
    public float shiverAmount = 0.5f; // Maximum amount of shivering
    public float baseShiverSpeed = 10f; // Base speed of the shivering effect
    public float timeOffsetMultiplier = 0.1f; // Time offset for each character

    [Header("Individual Speed Settings")]
    public float[] individualShiverSpeeds; // Array to hold individual speeds for each character

    [Header("Glitch/Stretch Settings")]
    public float maxHorizontalShift = 10f; // Increased shift for a more dramatic effect
    public float maxStretchAmount = 0.2f; // Increased stretch amount for horror feel
    public float stretchHoldDuration = 0.15f; // Shorter hold for more abrupt transitions

    [Header("Stretch Chance Settings")]
    [Range(0f, 1f)]
    public float stretchChance = 0.7f; // Higher chance for more frequent stretch effects

    private Vector3[] originalVertices;
    private bool isStretching = false;

    private void Start()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        originalVertices = new Vector3[textInfo.meshInfo[0].vertices.Length];
        System.Array.Copy(textInfo.meshInfo[0].vertices, originalVertices, originalVertices.Length);

        // Initialize individual shiver speeds if not set
        if (individualShiverSpeeds.Length == 0 || individualShiverSpeeds.Length < textInfo.characterCount)
        {
            individualShiverSpeeds = new float[textInfo.characterCount];
            for (int i = 0; i < individualShiverSpeeds.Length; i++)
            {
                individualShiverSpeeds[i] = Random.Range(5f, 15f); // Random speed for each character
            }
        }

        StartCoroutine(ApplyGlitchEffect());
    }

    private void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            // Use individual speed for each character
            float timeOffset = (Time.time * individualShiverSpeeds[i]) + (i * timeOffsetMultiplier);
            float shiverAmountY = Mathf.Sin(timeOffset + Random.Range(-1f, 1f)) * shiverAmount; // Added randomness
            float shiverAmountX = Mathf.Sin(timeOffset * 2f + Random.Range(-1f, 1f)) * (Random.Range(0.5f, 1.5f) * shiverAmount * 0.5f); // Randomize shiver strength

            for (int j = 0; j < 4; ++j)
            {
                verts[charInfo.vertexIndex + j] += new Vector3(shiverAmountX, shiverAmountY, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }

        // Handle the glitch effect
        if (isStretching)
        {
            for (int i = 0; i < textInfo.meshInfo.Length; ++i)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textComponent.UpdateGeometry(meshInfo.mesh, i);
            }
        }
    }

    private IEnumerator ApplyGlitchEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f)); // Slower time between glitches

            if (!isStretching)
            {
                isStretching = true;

                bool stretch = Random.value < stretchChance;
                bool shift = Random.value > 0.5f;

                float targetStretch = stretch ? Random.Range(1f, 1f + maxStretchAmount) : 1f;
                float targetShift = shift ? Random.Range(-maxHorizontalShift, maxHorizontalShift) : 0;

                yield return StartCoroutine(GlitchEffect(targetStretch, targetShift));

                isStretching = false;
            }
        }
    }

    private IEnumerator GlitchEffect(float targetStretch, float targetShift)
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        float duration = Random.Range(0.05f, 0.15f); // Quicker transition for a glitchy feel
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            for (int i = 0; i < textInfo.characterCount; ++i)
            {
                var charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;

                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (int j = 0; j < 4; ++j)
                {
                    var orig = originalVertices[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = orig + new Vector3(targetShift * (t / duration), 0, 0)
                    + new Vector3((orig.x - textInfo.characterInfo[0].origin) * (targetStretch - 1) * (t / duration), 0, 0);
                }
            }

            for (int i = 0; i < textInfo.meshInfo.Length; ++i)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textComponent.UpdateGeometry(meshInfo.mesh, i);
            }

            yield return null;
        }

        yield return new WaitForSeconds(stretchHoldDuration);

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            for (int i = 0; i < textInfo.characterCount; ++i)
            {
                var charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;

                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (int j = 0; j < 4; ++j)
                {
                    var orig = originalVertices[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = orig + new Vector3(targetShift * (1 - t / duration), 0, 0)
                    + new Vector3((orig.x - textInfo.characterInfo[0].origin) * (targetStretch - 1) * (1 - t / duration), 0, 0);
                }
            }

            for (int i = 0; i < textInfo.meshInfo.Length; ++i)
            {
                var meshInfo = textInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                textComponent.UpdateGeometry(meshInfo.mesh, i);
            }

            yield return null;
        }
    }
}
