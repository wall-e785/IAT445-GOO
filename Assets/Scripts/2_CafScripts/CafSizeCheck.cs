using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;

public class CafSizeCheck : MonoBehaviour
{
    public GameObject blockerOne;
    public GameObject blockerTwo;
    private bool bigEnough = false;
    private bool unlocked = false;
    private bool displaying = false;
    private bool soundPlayed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sizeCheck(bool val)
    {
        bigEnough = val;

        if (bigEnough && !unlocked)
        {
            unlocked = true;
            blockerOne.GetComponent<XRGrabInteractable>().enabled = true;
            blockerTwo.GetComponent<XRGrabInteractable>().enabled = true;


            Rigidbody rb1 = blockerOne.GetComponent<Rigidbody>();
            Rigidbody rb2 = blockerTwo.GetComponent<Rigidbody>();

            rb1.constraints = RigidbodyConstraints.None;
            rb2.constraints = RigidbodyConstraints.None;

            if (!soundPlayed)
            {
                AudioManager.Instance.PlaySound("caf-3");
                soundPlayed = true;
            }
        }
        else
        {
            unlocked = false;
            blockerOne.GetComponent<XRGrabInteractable>().enabled = false;
            blockerTwo.GetComponent<XRGrabInteractable>().enabled = false;


            Rigidbody rb1 = blockerOne.GetComponent<Rigidbody>();
            Rigidbody rb2 = blockerTwo.GetComponent<Rigidbody>();

            rb1.constraints = RigidbodyConstraints.FreezeAll;
            rb2.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !unlocked)
        {

            if (!displaying)
            {
                displaying = true;
                AudioManager.Instance.PlaySound("Negative");
                StartCoroutine(Display("Goo still hungry... Goo need MORE food..."));
            }

        }
    }

    IEnumerator Display(string text)
    {
        UIManager.Instance.setThought(text);
        yield return new WaitForSeconds(3);
        UIManager.Instance.setThought("");
        displaying = false;
    }
}
