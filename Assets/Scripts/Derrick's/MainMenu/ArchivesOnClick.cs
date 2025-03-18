using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ArchivesOnClick : MonoBehaviour
{
   public float cameraMoveDuration = 1.5f;
   public Vector3 cameraTargetPos;
   public float smoothTime = 0.2f;
   public RectTransform buttonToMove;        
   public float buttonTargetXPos;            
   public float buttonSmoothTime = 0.2f;     

   public RectTransform secondButton;        
   public float secondButtonTargetXPos;      
   public float secondButtonSmoothTime = 0.2f; 

   public RectTransform thirdButton;         
   public float thirdButtonTargetXPos;       
   public float thirdButtonSmoothTime = 0.2f;
   
   public RectTransform m1Button;         
   public float m1ButtonTargetXPos;       
   public float m1ButtonSmoothTime = 0.2f;
   
   public RectTransform m2Button;         
   public float m2ButtonTargetXPos;       
   public float m2ButtonSmoothTime = 0.2f;
   
   public RectTransform m3Button;         
   public float m3ButtonTargetXPos;       
   public float m3ButtonSmoothTime = 0.2f;
   
   public RectTransform m4Button;         
   public float m4ButtonTargetXPos;       
   public float m4ButtonSmoothTime = 0.2f;
   
   public RectTransform m5Button;         
   public float m5ButtonTargetXPos;       
   public float m5ButtonSmoothTime = 0.2f;
   public void OnButtonClick()
   {
      StartCoroutine(MoveCameraToTarget());
      StartCoroutine(MoveButtonToTarget(buttonToMove, buttonTargetXPos, buttonSmoothTime));
      StartCoroutine(MoveButtonToTarget(secondButton, secondButtonTargetXPos, secondButtonSmoothTime));
      StartCoroutine(MoveButtonToTarget(thirdButton, thirdButtonTargetXPos, thirdButtonSmoothTime));
      StartCoroutine(MoveButtonToTarget(m1Button, m1ButtonTargetXPos, m1ButtonSmoothTime));
      StartCoroutine(MoveButtonToTarget(m2Button, m2ButtonTargetXPos, m2ButtonSmoothTime));
      StartCoroutine(MoveButtonToTarget(m3Button, m3ButtonTargetXPos, m3ButtonSmoothTime));
      StartCoroutine(MoveButtonToTarget(m4Button, m4ButtonTargetXPos, m4ButtonSmoothTime));
      StartCoroutine(MoveButtonToTarget(m5Button, m5ButtonTargetXPos, m5ButtonSmoothTime));
   }

   private IEnumerator MoveCameraToTarget()
   {
      Camera mainCamera = Camera.main;
      Vector3 velocity = Vector3.zero; // Required by SmoothDamp to smoothly reach the target position
      float elapsedTime = 0f;

      // Smoothly move the camera to the target position using SmoothDamp
      while (elapsedTime < cameraMoveDuration)
      {
         mainCamera.transform.position =
            Vector3.SmoothDamp(mainCamera.transform.position, cameraTargetPos, ref velocity, smoothTime);
         elapsedTime += Time.deltaTime;
         yield return null;
      }

      // Ensure the camera reaches the exact target position
      mainCamera.transform.position = cameraTargetPos;
      
      
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
