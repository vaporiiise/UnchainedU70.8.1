using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;  
    public Transform bulletsParent; 
    public SpriteRenderer directionIndicator; 
    public Sprite upSprite, downSprite, leftSprite, rightSprite; 

    private Vector2Int selectedDirection; 
    private bool isDirectionSelected = false; 
    public GameObject rouletteWheel;
    public AudioClip shootSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3) && !isDirectionSelected)
        {
            StartCoroutine(StartRouletteWithAnimation());
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && isDirectionSelected)
        {
            rouletteWheel.SetActive(false);

            Shoot();
            audioSource.PlayOneShot(shootSound);

            ResetIndicator();
        }
    }
    
    IEnumerator StartRouletteWithAnimation()
    {
        rouletteWheel.SetActive(true);

        yield return new WaitForSeconds(1f); 

        StartRoulette();
    }



    void StartRoulette()
    {

        
        isDirectionSelected = true;

        int randomDir = Random.Range(0, 4);
        switch (randomDir)
        {
            case 0: 
                selectedDirection = Vector2Int.up;
                directionIndicator.sprite = upSprite;
                break;
            case 1: 
                selectedDirection = Vector2Int.down;
                directionIndicator.sprite = downSprite;
                break;
            case 2: 
                selectedDirection = Vector2Int.left;
                directionIndicator.sprite = leftSprite;
                break;
            case 3: 
                selectedDirection = Vector2Int.right;
                directionIndicator.sprite = rightSprite;
                break;
        }

        directionIndicator.gameObject.SetActive(true);
    }

    void Shoot()
    {
        if (bulletPrefab == null || bulletsParent == null) return;

        Quaternion bulletRotation = Quaternion.identity; 

        if (selectedDirection == Vector2.left)
        {
            bulletRotation = Quaternion.Euler(0, 0, 90); 
        }
        else if (selectedDirection == Vector2.right)
        {
            bulletRotation = Quaternion.Euler(0, 0, -90); 
        }
        else if (selectedDirection == Vector2.down)
        {
            bulletRotation = Quaternion.Euler(0, 0, 180); 
        }

        GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation, bulletsParent);

        TVBullet bulletScript = bullet.GetComponent<TVBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(selectedDirection); 
        }

        isDirectionSelected = false;
    }
    

    void ResetIndicator()
    {
        directionIndicator.gameObject.SetActive(false);
    }
}
