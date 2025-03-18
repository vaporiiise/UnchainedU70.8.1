using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{
    public Transform target;  // The target the camera follows
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Offset from the target's position

    void LateUpdate()
    {
        if (target != null)
        {
            // Set the camera position to the target position plus the offset
            transform.position = target.position + offset;
        }
    }
}
