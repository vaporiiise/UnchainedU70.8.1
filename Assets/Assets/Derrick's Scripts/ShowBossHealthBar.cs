using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBossHealthBar : MonoBehaviour
{
    public Canvas bossCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        bossCanvas.gameObject.SetActive(true);
    }
}
