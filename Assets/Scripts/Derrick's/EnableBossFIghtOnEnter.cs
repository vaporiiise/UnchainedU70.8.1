using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBossFIghtOnEnter : MonoBehaviour
{
    [SerializeField] public GameObject scriptToEnable; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            scriptToEnable.SetActive(true);
        }
    }
}
