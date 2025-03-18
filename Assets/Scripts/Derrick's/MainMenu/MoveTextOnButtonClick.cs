using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveTextOnButtonClick : MonoBehaviour
{
    public RectTransform buttonToMove;        
    public float buttonTargetXPos;            
    public float buttonSmoothTime = 0.2f;     

    public RectTransform secondButton;        
    public float secondButtonTargetXPos;      
    public float secondButtonSmoothTime = 0.2f; 

    public RectTransform thirdButton;         
    public float thirdButtonTargetXPos;       
    public float thirdButtonSmoothTime = 0.2f;

    private void OnButtonClick()
    {
        StartCoroutine(MoveButtonToTarget(buttonToMove, buttonTargetXPos, buttonSmoothTime));
        StartCoroutine(MoveButtonToTarget(secondButton, secondButtonTargetXPos, secondButtonSmoothTime));
        StartCoroutine(MoveButtonToTarget(thirdButton, thirdButtonTargetXPos, thirdButtonSmoothTime));
    }
    private IEnumerator MoveButtonToTarget(RectTransform button, float targetXPos, float smoothTime)
    {
        Vector3 velocity = Vector3.zero;
        Vector3 startPos = button.anchoredPosition;
        Vector3 targetPos = new Vector3(targetXPos, startPos.y, startPos.z);

        while (Vector3.Distance(button.anchoredPosition, targetPos) > 0.1f)
        {
            button.anchoredPosition = Vector3.SmoothDamp(button.anchoredPosition, targetPos, ref velocity, smoothTime);
            yield return null;
        }

        // Ensure the button reaches the exact target position
        button.anchoredPosition = targetPos;
    }
}
