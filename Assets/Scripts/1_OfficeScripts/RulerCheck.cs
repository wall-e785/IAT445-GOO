using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;

public class RulerCheck : MonoBehaviour
{
    public GameObject requiredFood;
    private bool unlocked = false;
    private bool displaying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (requiredFood == null && !unlocked) {
            unlocked = true;
            GetComponent<XRGrabInteractable>().enabled = true;

            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !unlocked)
        {
            
            if (!displaying) {
                displaying = true;
                StartCoroutine(Display("Goo... Need Food..."));
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
