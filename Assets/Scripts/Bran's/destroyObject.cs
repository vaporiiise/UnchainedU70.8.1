using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D borderCol)
    {
        Destroy(borderCol.gameObject);
    }
}