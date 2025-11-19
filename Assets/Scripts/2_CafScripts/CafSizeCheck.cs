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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bigEnough && !unlocked)
        {
            unlocked = true;
            blockerOne.GetComponent<XRGrabInteractable>().enabled = true;
            blockerTwo.GetComponent<XRGrabInteractable>().enabled = true;


            Rigidbody rb1 = blockerOne.GetComponent<Rigidbody>();
            Rigidbody rb2 = blockerTwo.GetComponent<Rigidbody>();

            rb1.constraints = RigidbodyConstraints.None;
            rb2.constraints = RigidbodyConstraints.None;
        }
    }

    public void sizeCheck()
    {
        bigEnough = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !unlocked)
        {

            if (!displaying)
            {
                displaying = true;
                StartCoroutine(Display("Goo... Need MORE Food..."));
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
