using UnityEngine;
using System.Collections;

public class LoadZoneTrigger : MonoBehaviour
{
    //zones to load
    public GameObject warehouseZone;
    public GameObject securityZone;

    //things to trigger
    public Animator warehouseDoor;
    public Animator securityDoor;
    public GameObject locomotion;

    private bool loaded = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !loaded)
        {
            loaded = true;
            locomotion.SetActive(false);
            StartCoroutine(startLoad());
        }
    }

    IEnumerator startLoad()
    {
        warehouseDoor.SetBool("Opening", false);
        AudioManager.Instance.PlaySound("Warehouse Door Close");
        yield return new WaitForSeconds(6);
        warehouseZone.SetActive(false);
        securityZone.SetActive(true);
        yield return new WaitForEndOfFrame();
        securityDoor.SetBool("Opening", true);
        AudioManager.Instance.PlaySound("Security Warehouse Door Open");
        locomotion.SetActive(true);
    }
}
