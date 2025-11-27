using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour
{
    public float fadeDuration = 2;
    public Color fadeColor;
    private Renderer rend;
    private char direction; //I/O to represent In/Out

    void Awake()
    {
        rend = GetComponent<Renderer>();

    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public void FadeIn()
    {
        direction = 'I';
        Fade(1, 0);
    }

    public void FadeOut()
    {
        direction = 'O';
        Fade(0, 1);
    }

    IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            rend.material.SetColor("_Color", newColor);
            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);

        yield return null;

        if(direction == 'I')
        {
            this.gameObject.SetActive(false);
        }
    }
}
