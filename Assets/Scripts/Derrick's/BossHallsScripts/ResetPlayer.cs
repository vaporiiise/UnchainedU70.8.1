using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPlayer : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.HasKey("SavedX"))
        {
            float x = PlayerPrefs.GetFloat("SavedX");
            float y = PlayerPrefs.GetFloat("SavedY");
            float z = PlayerPrefs.GetFloat("SavedZ");
            transform.position = new Vector3(x, y, z);
        }

        PlayerPrefs.GetInt("currentHealth");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void SaveBossHealth()
    {
        
    }
}