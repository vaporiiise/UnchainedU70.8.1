using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWhenPlayerisNear : MonoBehaviour
{
    public string targetTag = "BossRoom";
    public Transform player;
    public float threshold = 0.5f;
    public Animator animator;
    public AudioClip animationSound;
    public Color gizmoColor = Color.green;

    private bool isNear = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        bool playerClose = false;

        foreach (GameObject target in targets)
        {
            if (Vector3.Distance(target.transform.position, player.position) <= threshold)
            {
                playerClose = true;
                break;
            }
        }

        if (playerClose != isNear)
        {
            isNear = playerClose;
            animator.SetBool("isNear", isNear);

            if (isNear) 
            {
                if (animationSound != null && !audioSource.isPlaying)
                {
                    SFXManager.instance.PlaySFX(animationSound);
                    //audioSource.PlayOneShot(animationSound);
                }
            }
            else 
            {
                audioSource.Stop();
            }
        }
    }

    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(player.position, threshold); // Draws ONLY one radius
        }
    }
}