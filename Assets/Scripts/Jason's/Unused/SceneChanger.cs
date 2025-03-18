using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
   
    public int scenes; 

    public void ChangeScene()
    {
        SceneManager.LoadScene(scenes);
    }
}
