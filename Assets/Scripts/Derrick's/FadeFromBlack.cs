using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeFromBlack : MonoBehaviour
{
    public Camera mainCamera; // The camera to measure distance from
    public float fadeDistance = 15f; // The distance at which fading starts
    public float fadeDuration = 2f; // The time it takes to fully fade in
    private Material[] materials; // Store materials of all child objects

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        // Get all materials of the children
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        materials = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);

        // Calculate alpha based on distance
        float alpha = Mathf.Clamp01(1 - (distance - fadeDistance) / fadeDuration);

        // Update materials' alpha
        foreach (Material mat in materials)
        {
            if (mat.HasProperty("_Color"))
            {
                Color color = mat.color;
                color.a = alpha;
                mat.color = color;
            }
        }
    }
}