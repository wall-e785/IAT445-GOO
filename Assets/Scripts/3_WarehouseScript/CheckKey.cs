using UnityEngine;

public class CheckKey : MonoBehaviour
{

    [SerializeField] private Animator doorController;
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WarehouseKey" && !activated)
        {
            Debug.Log("door opening");
            activated = true;
            doorController.SetBool("Opening", true);
            Destroy(other.gameObject);
        }
    }
}
