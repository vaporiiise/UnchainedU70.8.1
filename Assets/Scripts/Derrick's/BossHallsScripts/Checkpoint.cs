using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject objectToActivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("SavedX", transform.position.x);
            PlayerPrefs.SetFloat("SavedY", transform.position.y);
            PlayerPrefs.Save();

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }
}