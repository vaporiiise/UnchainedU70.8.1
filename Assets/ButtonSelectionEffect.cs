using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class ButtonSelectionEffect : MonoBehaviour
{
    public Image buttonImage; // Assign the button's Image component
    public Sprite defaultSprite; // Normal button image
    public Sprite highlightedSprite; // Highlighted (hovered/selected) image
    public Animator buttonAnimator; // Animator for selection animation

    private void Start()
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = defaultSprite; // Set default sprite
        }
    }

    // When the button is selected (keyboard/controller navigation)
    public void OnSelect(BaseEventData eventData)
    {
        PlaySelectionAnimation();
    }

    // When the button is deselected
    public void OnDeselect(BaseEventData eventData)
    {
        SetDefaultSprite();
    }

    // When the mouse hovers over the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetHighlightedSprite();
    }

    // When the mouse exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        SetDefaultSprite();
    }

    private void SetHighlightedSprite()
    {
        if (buttonImage != null && highlightedSprite != null)
        {
            buttonImage.sprite = highlightedSprite;
        }
    }

    private void SetDefaultSprite()
    {
        if (buttonImage != null && defaultSprite != null)
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    private void PlaySelectionAnimation()
    {
        if (buttonAnimator != null)
        {
            buttonAnimator.SetTrigger("Selected"); // Ensure an animation trigger is set in Animator
        }
    }
}