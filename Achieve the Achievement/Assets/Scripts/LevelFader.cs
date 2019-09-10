using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFader : MonoBehaviour
{
    public RawImage FadeImage;
    public float FadeSpeed = 0.8f;
    public enum FadeDirection
    {
        In, //Alpha = 1
        Out //Alpha = 0
    }

    public IEnumerator FadeRoutine(FadeDirection fadeDirection)
    {
        float alpha = (fadeDirection == FadeDirection.Out) ? 1 : 0;
        float fadeEndValue = (fadeDirection == FadeDirection.Out) ? 0 : 1;

        Debug.Log(fadeEndValue);
        if (fadeDirection == FadeDirection.Out)
        {
            while (alpha >= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
            FadeImage.enabled = false;
        }
        else
        {
            FadeImage.enabled = true;
            while(alpha <= fadeEndValue)
            {
                SetColorImage(ref alpha, fadeDirection);
                yield return null;
            }
            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, alpha);
        }
    }

    private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
    {
        FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / FadeSpeed) * ((fadeDirection == FadeDirection.Out) ? -1 : 1);
    }
}
