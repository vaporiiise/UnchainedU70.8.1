using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameButton : MonoBehaviour
{
    public void Quit()
    {
        if (Application.isPlaying && !Application.isEditor)
        {
            Application.Quit();
        }
    }
}