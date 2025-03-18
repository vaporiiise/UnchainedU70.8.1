using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator2 : MonoBehaviour
{
    private Animator anim; 

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isMovingUp = vertical > 0.1f;
        bool isMovingDown = vertical < -0.1f;
        bool isMovingLeft = horizontal < -0.1f;
        bool isMovingRight = horizontal > 0.1f;

        anim.SetBool("PlayerUp", isMovingUp);
        anim.SetBool("PlayerDown", isMovingDown);
        anim.SetBool("PlayerLeft", isMovingLeft);
        anim.SetBool("PlayerRight", isMovingRight);
    }


}