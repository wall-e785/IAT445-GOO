using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class XROriginScale : MonoBehaviour
{
    public Transform xrOriginTransform;
    public Transform camera;

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
            int currScene = SceneManager.GetActiveScene().buildIndex;
            if (currScene == 1)
            {
                //xrOriginTransform.localScale += new Vector3(.1f, .1f, .1f);
                GrowPlayer(.05f);
            }else if(currScene == 2)
            {
                GrowPlayer(.01f);
            }else
            {
                //xrOriginTransform.localScale += new Vector3(.05f, .05f, .05f);
                GrowPlayer(.05f);

                //TEMP TESTING FOR CAM
                //xrOriginTransform.localPosition += new Vector3(.1f, .1f, .1f);
            }
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

    private void GrowPlayer(float amnt)
    {

        // Adjust CharacterController, adjust the height and radius of body manually based off the shrink factor
        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.height = cc.height+ amnt;//multiplies the character controller height by shrink factor
            cc.radius = cc.radius+ amnt;//multiplies the character controller body radius by shrink factor
            cc.center = new Vector3(cc.center.x, cc.height, cc.center.z);
            Debug.Log($"CharacterController updated: height = {cc.height}, radius = {cc.radius}, center = {cc.center}");
        }
        else
        {
            Debug.LogWarning("CharacterController not found");
        }

        // Adjust Camera Offset height, moves the players view down as origin and the controls shrink. makes it appear smaller
        Transform cameraOffset = transform.Find("Camera Offset");
        if (cameraOffset != null)
        {
            Vector3 offsetPos = cameraOffset.localPosition;
            offsetPos.y += amnt;//multiply the verticle position of the camera by the shrink factor
            //offsetPos.y = Mathf.Min(offsetPos.y, 2); // Clamp to avoid going underground
            cameraOffset.localPosition = offsetPos;
            Debug.Log("Camera Offset height adjusted to: " + offsetPos.y);
        }
        else
        {
            Debug.LogWarning("Camera Offset not found");
        }
    }
}
