using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using System.Collections;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;
using UnityEngine.Events;
using NUnit.Framework.Constraints;
using System;

//i initially wrote this script based off of this tutorial for the food: https://www.youtube.com/watch?v=7dj1m0Izyi0
//following this, i tried to modify it by adding the scale/position which did not work. The update, ontriggerenter were modified using Microsoft Copilot
//The LiftPlayer and ScalePlayer IEnumerators were written fully by Microsoft Copilot.
public class MouthTrigger : MonoBehaviour
{
    public Transform cameraTransform;
    public float height = 1;
    private float maxHeight;

    public InputActionReference translateYAxis;
    public Transform xrOrigin; // Assign this to your XR Origin GameObject

    // Scale increments
    Vector3 smallFoodScale = new Vector3(.3f, .3f, .3f);
    Vector3 mediumFoodScale = new Vector3(.5f, .5f, .5f);
    Vector3 largeFoodScale = new Vector3(.8f, .8f, .8f);

    // Lift amounts
    float smallLift = 0.3f;
    float mediumLift = 0.6f;
    float largeLift = 1.0f;

    // Other Triggers
    private int eatCount = 0;
    public UnityEvent<Vector3, float> onTriggered;
    public UnityEvent CafSizeCheck;



    void Start()
    {

    }

    void Update()
    {
        //// Optional: clamp camera height if needed
        //if (cameraTransform.localPosition.y >= 3)
        //{
        //    cameraTransform.localPosition = new Vector3(
        //        cameraTransform.localPosition.x,
        //        3,
        //        cameraTransform.localPosition.z
        //    );
        //}
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Was triggered by: " + other.name + " " + other.tag);

        float liftAmount = 0f;
        Vector3 scaleIncrement = Vector3.zero;
        float delayIncrement = 0;

        if(other.CompareTag("SmallFood") || other.CompareTag("MediumFood") || other.CompareTag("LargeFood") || other.CompareTag("Rat"))
        {
            switch (other.tag)
            {
                case "SmallFood":
                    liftAmount = smallLift;
                    scaleIncrement = smallFoodScale;
                    delayIncrement = .2f;
                    eatCount++;
                    break;
                case "MediumFood":
                    liftAmount = mediumLift;
                    scaleIncrement = mediumFoodScale;
                    delayIncrement = .4f;
                    eatCount+=2;
                    break;
                case "LargeFood":
                    liftAmount = largeLift;
                    scaleIncrement = largeFoodScale;
                    delayIncrement = .6f;
                    eatCount+=3;
                    break;
                case "Rat":
                    liftAmount = largeLift;
                    scaleIncrement = largeFoodScale;
                    delayIncrement = .6f;
                    eatCount += 3;
                    AudioManager.Instance.PlaySound("EW");
                    break;
            }

            Debug.Log("EatCount: " + eatCount);

            //switch to check for eatCount for each scene's specific puzzle.
            switch(SceneManager.GetActiveScene().name){
                case "1_Level": //checks for the office level
                    if (eatCount >= 1)
                    {
                        AudioManager.Instance.PlaySound("office-3");
                        UIManager.Instance.setText("Create a path to the vent.");
                    }
                    break;
                case "2_Level": //checks for the caf level
                    if(eatCount >= 18)
                    {
                        CafSizeCheck?.Invoke();
                        UIManager.Instance.setText("Escape through the garbage chute.");
                    }
                    break;
            }




            if (!other.CompareTag("Rat"))
            {
                Destroy(other.transform.parent.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
            onTriggered?.Invoke(scaleIncrement, delayIncrement);

            // Animate lift and scale
            //Vector3 targetPosition = xrOrigin.position + new Vector3(0, liftAmount, 0);
            //Vector3 targetScale = xrOrigin.localScale + scaleIncrement;

            //StartCoroutine(LiftPlayer(targetPosition, 1.5f));
            //StartCoroutine(ScalePlayer(targetScale, 1.5f));
        }

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

    IEnumerator playDelay(string clipName, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySound(clipName);
    }
}