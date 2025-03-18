using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnHold : MonoBehaviour
{
    public Animator animator;  // Assign your Animator in the Inspector
    private bool isHoldingEsc = false;
    private float idleTimer = 0f;
    public float idleTimeout = 69f; // Time before auto scene change
    private bool animationFinished = false;

    void Update()
    {
        idleTimer += Time.deltaTime; // Track idle time

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isHoldingEsc = true;
            idleTimer = 0f; // Reset idle timer on input
            animator.Play("HOLDCIRCLE", 0, 0); // Start animation from beginning
            animationFinished = false; // Reset animation flag
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isHoldingEsc = false;
            animator.Play("IdleCircle", 0, 0); // Reset animation (optional)
            animationFinished = false; // Prevent scene transition
        }

        // Check if animation has finished playing
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (isHoldingEsc && stateInfo.IsName("HOLDCIRCLE") && stateInfo.normalizedTime >= 1.0f && !animationFinished)
        {
            animationFinished = true;
            LoadNextScene();
        }

        if (idleTimer >= idleTimeout) // If no input for 71 seconds
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with your actual scene name
    }
}