using UnityEngine;
using System.Collections;

public class ChuteTrigger : MonoBehaviour
{
    public Animator chuteAnimator;   // Assign ChuteDoor's Animator here
    public GameObject sceneTrigger;
    private bool playerInZone = false;
    private bool hasOpened = false;
    private bool sizeCheck = false;
   

    //void Update()
    //{
        

    //    if (playerInZone && !hasOpened && Input.GetKeyDown(KeyCode.P))
    //    {
    //        chuteAnimator.SetTrigger("Open");
    //        hasOpened = true;
    //        Debug.Log("Chute opened!");
    //    }
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerInZone = true;
    //        Debug.Log("Player entered chute zone");
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerInZone = false;
    //        Debug.Log("Player left chute zone");
    //    }
    //}

    public void bigEnough(bool val)
    {
        sizeCheck = val;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player") && !hasOpened && sizeCheck)
        {
            chuteAnimator.SetTrigger("Open");
            hasOpened = true;
            Debug.Log("Chute opened!");
            AudioManager.Instance.PlaySound("NoBone");
            StartCoroutine(activateSceneTrigger());
        }
    }

    IEnumerator activateSceneTrigger()
    {
        yield return new WaitForSeconds(4);
        sceneTrigger.SetActive(true);
    }

}
