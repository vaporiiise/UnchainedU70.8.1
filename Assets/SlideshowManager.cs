using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideshowManager : MonoBehaviour
{
    public Image displayImage;  // Reference to the UI Image component
    public Sprite[] slides;     // Array of images for the slideshow
    private int currentIndex = 0;

    void Start()
    {
        if (slides.Length > 0)
        {
            displayImage.sprite = slides[currentIndex];
        }
    }

    public void NextSlide()
    {
        currentIndex = (currentIndex + 1) % slides.Length;
        displayImage.sprite = slides[currentIndex];
    }

    public void PreviousSlide()
    {
        currentIndex = (currentIndex - 1 + slides.Length) % slides.Length;
        displayImage.sprite = slides[currentIndex];
    }
}