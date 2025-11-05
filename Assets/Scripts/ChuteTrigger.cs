using UnityEngine;

public class ChuteTrigger : MonoBehaviour
{
    public Animator chuteAnimator;   // Assign ChuteDoor's Animator here
    private bool playerInZone = false;
    private bool hasOpened = false;

    void Update()
    {
        

            if (playerInZone && !hasOpened && Input.GetKeyDown(KeyCode.P))
        {
            chuteAnimator.SetTrigger("Open");
            hasOpened = true;
            Debug.Log("Chute opened!");
        }
    }

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

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player") && !hasOpened)
        {
            chuteAnimator.SetTrigger("Open");
            hasOpened = true;
            Debug.Log("Chute opened!");
        }
    }

}
