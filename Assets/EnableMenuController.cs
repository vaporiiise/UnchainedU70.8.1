using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMenuController : MonoBehaviour
{
    public GameObject targetObject; // The object to disable/enable
    public Canvas targetCanvas; // The canvas to check

    void Update()
    {
        if (targetCanvas != null && targetObject != null)
        {
            if (targetCanvas.gameObject.activeSelf)
            {
                targetObject.SetActive(false);
            }
            else
            {
                targetObject.SetActive(true);
            }
        }
    }
}
