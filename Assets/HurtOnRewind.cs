using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnRewind : MonoBehaviour
{
    NHPlayerHealth playerHealth;
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<NHPlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
