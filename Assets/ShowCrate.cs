using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCrate : MonoBehaviour
{
    public string targetTag = "ObjToPush";  // Tag of the first object
    public Transform player;  // Reference to the player
    public float threshold = 0.5f;  // Distance threshold
    public GameObject spriteObject; // The sprite GameObject

    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        bool isNear = false;

        foreach (GameObject target in targets)
        {
            if (Vector3.Distance(target.transform.position, player.position) <= threshold)
            {
                isNear = true;
                break;
            }
        }

        spriteObject.SetActive(isNear);
    }
}
