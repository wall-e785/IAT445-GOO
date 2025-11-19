using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CatBotStun : MonoBehaviour
{

    public GameObject locomotion;
    private bool soundPlayed = false;

    public UnityEvent<Vector3, float> onTriggered;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            locomotion.SetActive(false);
            if (!soundPlayed)
            {
                AudioManager.Instance.PlaySound("caf-death");
                soundPlayed = true;
            }
            onTriggered?.Invoke(new Vector3(.5f, .5f, .5f) ,.4f);
            StartCoroutine(stunDelay());
        }
    }

    IEnumerator stunDelay()
    {
        yield return new WaitForSeconds(2);
        locomotion.SetActive(true);
    }

}