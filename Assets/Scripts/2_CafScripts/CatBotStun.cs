using UnityEngine;
using System.Collections;

public class CatBotStun : MonoBehaviour
{

    public GameObject simulator;
    private bool soundPlayed = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            simulator.SetActive(false);
            if (!soundPlayed)
            {
                AudioManager.Instance.PlaySound("caf-death");
                soundPlayed = true;
            }
            StartCoroutine(stunDelay());
        }
    }

    IEnumerator stunDelay()
    {
        yield return new WaitForSeconds(2);
        simulator.SetActive(true);
    }

}