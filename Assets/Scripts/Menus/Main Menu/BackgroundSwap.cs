using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSwap : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite[] backgroundSprites;
    public float fadeDuration = 1f;
    public float displayDuration = 3f;

    private int currentImageIndex = 0;
    private Color blackColor = Color.black;
    private Color clearColor = Color.white;

    void Start()
    {
        // Start background fade and swap
        StartCoroutine(FadeAndSwapBackgrounds());
    }

    IEnumerator FadeAndSwapBackgrounds()
    {
        while (true)
        {
            // display current background
            backgroundImage.color = clearColor;

            // wait for duration
            yield return new WaitForSeconds(displayDuration);

            // fade to black
            yield return StartCoroutine(FadeToColor(blackColor, fadeDuration));

            // swap image
            currentImageIndex = (currentImageIndex + 1) % backgroundSprites.Length;
            backgroundImage.sprite = backgroundSprites[currentImageIndex];

            // fade back in
            yield return StartCoroutine(FadeToColor(clearColor, fadeDuration));
        }
    }

    IEnumerator FadeToColor(Color targetColor, float duration)
    {
        Color startColor = backgroundImage.color;
        float time = 0;

        while (time < duration)
        {
            backgroundImage.color = Color.Lerp(startColor, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        backgroundImage.color = targetColor;
    }
}