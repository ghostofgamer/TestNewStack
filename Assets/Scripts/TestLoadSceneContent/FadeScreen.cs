using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public Image fadeImage;
    public float fadeSpeed = 1f;
    
    void Start()
    {
        // Авто FadeIn при старте сцены
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeOut()
    {
        float t = fadeImage.color.a;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, t);
            yield return null;
        }
        fadeImage.color = Color.black;
    }

    public IEnumerator FadeIn()
    {
        float t = fadeImage.color.a;
        
        while (t > 0f)
        {
            t -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(0, 0, 0, t);
            yield return null;
        }
        
        fadeImage.color = Color.clear;
    }
}
