using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMenuCanvas : MonoBehaviour
{
    public Canvas targetCanvas; 
    public Canvas canvasToEnable; 
    void Update()
    {
        if (targetCanvas != null && canvasToEnable != null)
        {
            if (!targetCanvas.gameObject.activeSelf)
            {
                canvasToEnable.gameObject.SetActive(true);
            }
        }
    }
}

