using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [Header("Trigger Area Settings")]
    public Vector3 areaMin;
    public Vector3 areaMax;
    public Color gizmoColor = new Color(0f, 1f, 0f, 0.25f);

    private bool playerInArea = false;
    private float timeInArea = 0f;
    public float triggerDuration = 5f;
    public DialogueSystem dialogueSystem;

    [Header("Script Control")]
    public MonoBehaviour scriptToDisable; // Assign the script to disable when dialogue is active

    void Update()
    {
        Vector3 playerPosition = PlayerPosition();
        bool isInArea = IsPlayerInArea(playerPosition);

        if (isInArea)
        {
            playerInArea = true;
            timeInArea += Time.deltaTime;

            if (timeInArea >= triggerDuration)
            {
                OnPlayerTriggered();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartDialogue();
            }
        }
        else
        {
            if (playerInArea)
            {
                playerInArea = false;
                timeInArea = 0f;
            }
        }

        // Disable script while dialogue is active
        if (scriptToDisable != null && dialogueSystem != null)
        {
            scriptToDisable.enabled = !dialogueSystem.IsDialogueActive();
        }
    }

    private bool IsPlayerInArea(Vector3 position)
    {
        return position.x >= areaMin.x && position.x <= areaMax.x &&
               position.y >= areaMin.y && position.y <= areaMax.y &&
               position.z >= areaMin.z && position.z <= areaMax.z;
    }

    private Vector3 PlayerPosition()
    {
        GameObject player = GameObject.FindWithTag("Player");
        return player != null ? player.transform.position : Vector3.zero;
    }

    private void OnPlayerTriggered()
    {
        Debug.Log("Player has been in the area for " + triggerDuration + " seconds!");
    }

    private void StartDialogue()
    {
        if (dialogueSystem != null)
        {
            if (dialogueSystem.IsDialogueActive()) 
            {
                dialogueSystem.NextLine(); // Go to the next line
            }
            else
            {
                dialogueSystem.BeginDialogue();
            }
        }
        else
        {
            Debug.LogWarning("DialogueSystem is not assigned in " + gameObject.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube((areaMin + areaMax) / 2, areaMax - areaMin);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((areaMin + areaMax) / 2, areaMax - areaMin);
    }
}
