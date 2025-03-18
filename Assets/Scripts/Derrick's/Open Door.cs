using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Transform objectToDetect;   // The object you want to detect (e.g., player or other objects)
    public Transform objectToDetect2;
    public Transform areaCenter;       // Center point of your trigger area (use an empty GameObject for this)
    public Transform areaCenter2;
    public float detectionRadius = 5f;
    public GameObject targetObject;// The radius defining the area
    

    
    public GameObject objectToDisable;
    public Vector3 newPosition;
    //public GameObject objectToDestroy;
    //public GameObject objectToDisable2;

    private bool isArea1Triggered = false;
    private bool isArea2Triggered = false;
    

    void Update()
    {
        // Check if the object is within the radius of area 1
        if (Vector3.Distance(objectToDetect.position, areaCenter.position) <= detectionRadius)
        {
            isArea1Triggered = true;
        }
        else
        {
            isArea1Triggered = false;
        }

       
        if (Vector3.Distance(objectToDetect2.position, areaCenter2.position) <= detectionRadius) 
         {
             isArea2Triggered = true;
         } else {
            isArea2Triggered = false;
         }

        //Check if both areas are triggered (if using multiple areas)
        if (isArea1Triggered  && isArea2Triggered ) 
        {
            objectToDisable.SetActive(false);
            //objectToDisable.SetActive(false);
            //objectToDisable2.SetActive(false);
            //targetObject.tag = "Untagged";
            //Destroy(objectToDestroy);
            targetObject.transform.position += new Vector3(0, -3, 0) * Time.deltaTime;
            
        }
    }
}
