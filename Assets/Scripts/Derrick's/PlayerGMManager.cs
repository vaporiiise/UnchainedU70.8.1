using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGMManager : MonoBehaviour
{
    private Vector3 defaultPosition = new Vector3(-23.5f, -1.5f, 0); 

    void Start()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.savedPlayerPosition != Vector3.zero)
            {
                transform.position = GameManager.Instance.savedPlayerPosition;
                Debug.Log("Player position restored: " + transform.position);
            }
            else
            {
                transform.position = defaultPosition;
                Debug.Log("No saved position found. Default position set: " + transform.position);
            }
        }
        else
        {
            transform.position = defaultPosition;
            Debug.Log("GameManager not found. Default position set: " + transform.position);
        }
    }
}
