using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CatBotStun : MonoBehaviour
{

    public GameObject locomotion;
    private bool soundPlayed = false;

    public UnityEvent<Vector3, float> onTriggered;
    private bool stunned = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
            if (!soundPlayed)
            {
                AudioManager.Instance.PlaySound("caf-death");
                soundPlayed = true;
            }

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