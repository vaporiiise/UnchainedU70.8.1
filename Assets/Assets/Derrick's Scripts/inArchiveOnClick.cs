using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class inArchiveOnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject targetObject; // The object to move
    public float hoverOffset = 1.0f; // Distance to move along the Z-axis
    public float moveSpeed = 5.0f; // Speed of the movement

    private Vector3 originalPosition; // Store the original position
    private Vector3 targetPosition; // Position to move towards
    private bool isHovering = false; // Flag to track hover state

    void Start()
    {
        if (targetObject != null)
        {
            originalPosition = targetObject.transform.position;
            targetPosition = originalPosition;
        }
    }

    void Update()
    {
        if (targetObject != null)
        {
            // Smoothly interpolate the position
            targetObject.transform.position = Vector3.Lerp(targetObject.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetObject != null)
        {
            targetPosition = originalPosition + new Vector3(0, 0, hoverOffset); // Set target position for hover
            isHovering = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetObject != null)
        {
            targetPosition = originalPosition; // Reset target position
            isHovering = false;
        }
    }
}