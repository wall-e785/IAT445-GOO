using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SecurityBotStun : MonoBehaviour
{
    public GameObject locomotion;

    public UnityEvent<Vector3, float> onTriggered;
    public static bool stunned = false; //static so if multiple bots swam player, it won't repeat

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!stunned)
            {
                stunned = true;
                AudioManager.Instance.PlaySound("Negative");
                locomotion.SetActive(false);
                onTriggered?.Invoke(new Vector3(.5f, .5f, .5f), .5f);
                StartCoroutine(stunDelay());
            }

        }
    }

    IEnumerator stunDelay()
    {
        yield return new WaitForSeconds(2);
        locomotion.SetActive(true);
        stunned = false;
    }
}
