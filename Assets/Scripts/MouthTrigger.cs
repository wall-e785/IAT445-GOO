using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using System.Collections;

//i initially wrote this script based off of this tutorial for the food: https://www.youtube.com/watch?v=7dj1m0Izyi0
//following this, i tried to modify it by adding the scale/position which did not work. The update, ontriggerenter were modified using Microsoft Copilot
//The LiftPlayer and ScalePlayer IEnumerators were written fully by Microsoft Copilot.
public class MouthTrigger : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform floorReference;
    public float height = 1;
    private float maxHeight;

    public XRInteractionSimulator simulator;
    public InputActionReference translateYAxis;
    public float speed = 1.0f;
    public Transform xrOrigin; // Assign this to your XR Origin GameObject

    // Scale increments
    Vector3 smallFoodScale = new Vector3(.3f, .3f, .3f);
    Vector3 mediumFoodScale = new Vector3(.5f, .5f, .5f);
    Vector3 largeFoodScale = new Vector3(.8f, .8f, .8f);

    // Lift amounts
    float smallLift = 0.3f;
    float mediumLift = 0.6f;
    float largeLift = 1.0f;

    void Start()
    {
        if (floorReference != null)
        {
            maxHeight = floorReference.position.y + height;
        }
    }

    void Update()
    {
        // Optional: clamp camera height if needed
        if (cameraTransform.localPosition.y >= 3)
        {
            cameraTransform.localPosition = new Vector3(
                cameraTransform.localPosition.x,
                3,
                cameraTransform.localPosition.z
            );
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Was triggered by: " + other.name + " " + other.tag);

        float liftAmount = 0f;
        Vector3 scaleIncrement = Vector3.zero;

        if (other.CompareTag("SmallFood"))
        {
            liftAmount = smallLift;
            scaleIncrement = smallFoodScale;
        }
        else if (other.CompareTag("MediumFood"))
        {
            liftAmount = mediumLift;
            scaleIncrement = mediumFoodScale;
        }
        else if (other.CompareTag("LargeFood"))
        {
            liftAmount = largeLift;
            scaleIncrement = largeFoodScale;
        }
        else
        {
            return;
        }

        Destroy(other.transform.parent.gameObject);

        // Animate lift and scale
        Vector3 targetPosition = xrOrigin.position + new Vector3(0, liftAmount, 0);
        Vector3 targetScale = xrOrigin.localScale + scaleIncrement;

        //StartCoroutine(LiftPlayer(targetPosition, 1.5f));
        StartCoroutine(ScalePlayer(targetScale, 1.5f));
    }

    IEnumerator LiftPlayer(Vector3 targetPosition, float duration)
    {
        Vector3 initialPosition = xrOrigin.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            xrOrigin.position = Vector3.Lerp(initialPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        xrOrigin.position = targetPosition;
    }

    IEnumerator ScalePlayer(Vector3 targetScale, float duration)
    {
        Vector3 initialScale = xrOrigin.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            xrOrigin.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        xrOrigin.localScale = targetScale;
    }

}