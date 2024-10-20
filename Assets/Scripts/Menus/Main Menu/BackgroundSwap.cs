using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSwap : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite[] backgroundSprites;
    public float displayDuration = 4.5f;
    public float lastBackgroundDisplayDuration = 1.5f;

    private int currentBackgroundIndex = 0;

    void Start()
    {
        // Start background swapping
        StartCoroutine(SwapBackgrounds());
    }

    IEnumerator SwapBackgrounds()
    {
        while (true)
        {
            // display current background
            backgroundImage.sprite = backgroundSprites[currentBackgroundIndex];

            // determine display duration
            if (currentBackgroundIndex == backgroundSprites.Length - 1)
            {
                // shorter display duration
                yield return new WaitForSeconds(lastBackgroundDisplayDuration);
            }
            else
            {
                // normal display duration
                yield return new WaitForSeconds(displayDuration);
            }

            // swap to next background
            currentBackgroundIndex = (currentBackgroundIndex + 1) % backgroundSprites.Length;
        }
    }
}