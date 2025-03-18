using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Import DOTween


public class ShakeHealthBar : MonoBehaviour
{
    public RectTransform healthBar; // Assign the Health Bar's RectTransform

    public void HealthBarShake()
    {
        healthBar.DOShakePosition(0.3f, 5f, 20, 90, false, true);
    }
}