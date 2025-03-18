using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRewindOnArea : MonoBehaviour
{
    [Header("Area Settings")]
    public Vector3 areaMin;
    public Vector3 areaMax;
    public Color gizmoColor = new Color(1f, 0f, 0f, 0.3f); // Semi-transparent red

    [Header("Objects to Disable")]
    public MonoBehaviour scriptToDisable; // Assign a script in the Inspector
    public GameObject objectToDisable;   // Assign a GameObject in the Inspector

    private bool isInArea = false;

    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        Vector3 playerPosition = player.transform.position;
        bool wasInArea = isInArea;
        isInArea = IsPlayerInArea(playerPosition);

        if (isInArea && !wasInArea)
        {
            if (scriptToDisable != null && scriptToDisable.enabled)
            {
                scriptToDisable.enabled = false;
                Debug.Log($"{scriptToDisable.GetType().Name} disabled.");
            }

            if (objectToDisable != null && objectToDisable.activeSelf)
            {
                objectToDisable.SetActive(false);
                Debug.Log($"{objectToDisable.name} disabled.");
            }
        }
        else if (!isInArea && wasInArea)
        {
            if (scriptToDisable != null && !scriptToDisable.enabled)
            {
                scriptToDisable.enabled = true;
                Debug.Log($"{scriptToDisable.GetType().Name} re-enabled.");
            }

            if (objectToDisable != null && !objectToDisable.activeSelf)
            {
                objectToDisable.SetActive(true);
                Debug.Log($"{objectToDisable.name} re-enabled.");
            }
        }
    }

    private bool IsPlayerInArea(Vector3 position)
    {
        return position.x >= areaMin.x && position.x <= areaMax.x &&
               position.y >= areaMin.y && position.y <= areaMax.y &&
               position.z >= areaMin.z && position.z <= areaMax.z;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Vector3 center = (areaMin + areaMax) / 2;
        Vector3 size = areaMax - areaMin;
        Gizmos.DrawCube(center, size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
}
