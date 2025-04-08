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
            // Save position
            PlayerPrefs.SetFloat("SavedX", transform.position.x);
            PlayerPrefs.SetFloat("SavedY", transform.position.y);

            // Save health from bossHealth script
            bossHealth bh = other.GetComponent<bossHealth>();
            if (bh != null)
            {
                PlayerPrefs.SetInt("SavedHealth", bh.currentHealth);
            }

            PlayerPrefs.Save();

            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }

            Debug.Log("Checkpoint reached — position and health saved!");
        }
    }
}