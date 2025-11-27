using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;
using UnityEngine.Events;
using NUnit.Framework.Constraints;
using System;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

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
    public UnityEvent<bool> CafSizeCheck;
    public UnityEvent<bool> SecuritySizeCheck;
    private bool cafSoundPlayed = false;

    //visuals
    private ParticleSystem particles;


    void Start()
    {
        particles = GetComponent<ParticleSystem>();
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

        if(other.CompareTag("SmallFood") || other.CompareTag("MediumFood") || other.CompareTag("LargeFood") || other.CompareTag("Rat") || other.CompareTag("GooDrop"))
        {
            switch (other.tag)
            {
                case "SmallFood":
                case "GooDrop":
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

            if (particles)
            {
                particles.Play();
            }

            Debug.Log("EatCount: " + eatCount);

            //switch to check for eatCount for each scene's specific puzzle, as well as playing sound effects.
            switch(SceneManager.GetActiveScene().name){
                case "0.5_Level":
                    AudioManager.Instance.PlaySound("ChewSmall");
                    if(eatCount >=3)
                    {
                        AudioManager.Instance.PlaySound("Good Job");
                        UIManager.Instance.setThought("...Bye Friends...");
                    }
                    break;
                case "1_Level": //checks for the office level
                    AudioManager.Instance.PlaySound("ChewBig");
                    if (eatCount >= 1)
                    {
                        AudioManager.Instance.PlaySound("office-3");
                        AudioManager.Instance.PlaySound("Good Job");
                        AffordanceManager.Instance.progressParticles();
                    }
                    break;
                case "2_Level": //checks for the caf level
                    switch (other.tag)
                    {
                        case "SmallFood":
                            AudioManager.Instance.PlaySound("ChewSmall");
                            break;
                        case "MediumFood":
                        case "LargeFood":
                            AudioManager.Instance.PlaySound("ChewBig");
                            break;
                    }
                    if (eatCount >= 18)
                    {
                        CafSizeCheck?.Invoke(true);
                        UIManager.Instance.setText("Escape through the garbage chute.");
                        AudioManager.Instance.PlaySound("Good Job");
                        AudioManager.Instance.PlaySound("BurpBig");
                        UIManager.Instance.setThought("GOO POWER!");
                    }
                    else if (eatCount >= 9 && !cafSoundPlayed)
                    {
                        AudioManager.Instance.PlaySound("caf-2");
                        cafSoundPlayed = true;
                    }
                    else
                    {
                        CafSizeCheck?.Invoke(false);
                    }

                    //progress for player
                    if(eatCount < 6)
                    {
                        UIManager.Instance.setThought("Still weak... Need food...");
                    }else if (eatCount < 10)
                    {
                        UIManager.Instance.setThought("My stomach is only half full.");
                    }else if(eatCount < 18)
                    {
                        UIManager.Instance.setThought("Almost... Enough...");
                    }
                    break;
                case "3-4_Level":
                    switch (other.tag)
                    {
                        case "SmallFood":
                            AudioManager.Instance.PlaySound("ChewSmall");
                            break;
                        case "MediumFood":
                        case "LargeFood":
                            AudioManager.Instance.PlaySound("ChewBig");
                            break;
                    }
                    if (eatCount >= 24)
                    {
                        SecuritySizeCheck?.Invoke(true);
                        AudioManager.Instance.PlaySound("Good Job");
                        AudioManager.Instance.PlaySound("BurpBig");
                    }
                    else
                    {
                        SecuritySizeCheck?.Invoke(false);
                    }
                    break;
            }

            StartCoroutine(destroyDelay(other));

            onTriggered?.Invoke(scaleIncrement, delayIncrement);


            // Animate lift and scale
            //Vector3 targetPosition = xrOrigin.position + new Vector3(0, liftAmount, 0);
            //Vector3 targetScale = xrOrigin.localScale + scaleIncrement;

            //StartCoroutine(LiftPlayer(targetPosition, 1.5f));
            //StartCoroutine(ScalePlayer(targetScale, 1.5f));
        }

    }

    public int getEatCount()
    {
        return eatCount;
    }

    public void subtractEatCount(int count)
    {
        eatCount = eatCount - count;
        if (eatCount < 0) eatCount = 0;
        Debug.Log("Eat Count Subtracted to: " + eatCount);
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

    IEnumerator destroyDelay(Collider other)
    {

        if (other.CompareTag("Rat"))
        {

            //used to release the object from the XR grab before destroying
            XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();

            if (grab != null && grab.isSelected)
            {
                if (grab.firstInteractorSelecting != null)
                {
                    grab.interactionManager.SelectExit(grab.firstInteractorSelecting, grab);
                }
            }
            yield return null;

            Destroy(other.gameObject);
        }
        else
        {
            if (other.CompareTag("GooDrop"))
            {
                int soundToPlay = (int)UnityEngine.Random.Range(1, 8);
                string soundName = "Absorb" + soundToPlay;
                AudioManager.Instance.PlaySound(soundName);
            }

            //used to release the object from the XR grab before destroying
            XRGrabInteractable grab = other.transform.parent.GetComponent<XRGrabInteractable>();

            if (grab != null && grab.isSelected)
            {
                if (grab.firstInteractorSelecting != null)
                {
                    grab.interactionManager.SelectExit(grab.firstInteractorSelecting, grab);
                }
            }
            yield return null;

            Destroy(other.transform.parent.gameObject);
        }
    }
}