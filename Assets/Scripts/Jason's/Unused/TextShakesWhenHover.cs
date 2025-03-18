using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextShakesWhenHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text textComponent;

    [Header("Shiver Settings")]
    public float shiverAmount = 0.5f; // Maximum amount of shivering
    public float baseShiverSpeed = 10f; // Base speed of the shivering effect
    public float timeOffsetMultiplier = 0.1f; // Time offset for each character

    [Header("Individual Speed Settings")]
    public float[] individualShiverSpeeds; // Array to hold individual speeds for each character

    private Vector3[] originalVertices;
    private bool isHovered = false; // To track if the cursor is hovering over the text

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
    }

    private void Update()
    {
        if (isHovered)
        {
            textComponent.ForceMeshUpdate();
            var textInfo = textComponent.textInfo;

            for (int i = 0; i < textInfo.characterCount; ++i)
            {
                var charInfo = textInfo.characterInfo[i];

                if (!charInfo.isVisible)
                    continue;

                var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                // Calculate shivering effect
                float timeOffset = (Time.time * individualShiverSpeeds[i]) + (i * timeOffsetMultiplier);
                float shiverAmountY = Mathf.Sin(timeOffset + Random.Range(-1f, 1f)) * shiverAmount;
                float shiverAmountX = Mathf.Sin(timeOffset * 2f + Random.Range(-1f, 1f)) * (Random.Range(0.5f, 1.5f) * shiverAmount * 0.5f);

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
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        ResetTextPosition();
    }

    private void ResetTextPosition()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            for (int j = 0; j < 4; ++j)
            {
                verts[charInfo.vertexIndex + j] = originalVertices[charInfo.vertexIndex + j];
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
