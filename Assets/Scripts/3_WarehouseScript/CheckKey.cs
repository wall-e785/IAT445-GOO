using UnityEngine;

public class CheckKey : MonoBehaviour
{

    [SerializeField] private Animator doorController;
    private bool activated = false;
    private string tag;

    void Awake()
    {
        tag = gameObject.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tag && !activated)
        {
            //Debug.Log("door opening");
            activated = true;
            doorController.SetBool("Opening", true);
            AudioManager.Instance.PlaySound("ScanCard");
            Destroy(other.gameObject);

            if(tag == "WarehouseKey")
            {
                AudioManager.Instance.PlaySound("Warehouse Door Open");
            }
            else if(tag == "BlueKey")
            {
                AudioManager.Instance.PlaySound("Security Room Door Open");
            }
        }
    }
}
