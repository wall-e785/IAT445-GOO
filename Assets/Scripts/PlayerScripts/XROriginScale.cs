using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class XROriginScale : MonoBehaviour
{
    public Transform xrOriginTransform;
    private bool grow = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (grow)
        {
            xrOriginTransform.localScale += new Vector3(.1f, .1f, .1f);
        }
    }

    public void startGrow(Vector3 scaleIncrement, float delay)
    {
        //Vector3 targetScale = xrOriginTransform.localScale + scaleIncrement;

        //StartCoroutine(ScalePlayer(targetScale, 1.5f));
        grow = true;
        StartCoroutine(StopGrow(delay));
    }

    IEnumerator ScalePlayer(Vector3 targetScale, float duration)
    {

        Vector3 initialScale = xrOriginTransform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            xrOriginTransform.localScale += new Vector3(.1f, .1f, .1f); //= Vector3.Lerp(initialScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        xrOriginTransform.localScale = targetScale;
    }

    IEnumerator StopGrow(float delay)
    {
        yield return new WaitForSeconds(delay);
        grow = false;
    }
}
