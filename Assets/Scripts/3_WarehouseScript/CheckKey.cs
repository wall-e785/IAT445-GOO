using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


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
            StartCoroutine(delayDestroy(other));

            if(tag == "WarehouseKey")
            {
                AudioManager.Instance.PlaySound("Warehouse Door Open");
                AudioManager.Instance.PlaySound("Escape");
            }
            else if(tag == "BlueKey")
            {
                AudioManager.Instance.PlaySound("Security Room Door Open");
                UIManager.Instance.setText("Find the Pink Keycard to escape!");
                AffordanceManager.Instance.progressParticles();
            }else if(tag == "PinkKey")
            {
                AudioManager.Instance.PlaySound("Warehouse Door Open");
                AudioManager.Instance.PlaySound("GodDamnIt");
                doorController.SetBool("Opening", true);
            }
        }
    }

    IEnumerator delayDestroy(Collider other)
    {
        //used to release the object from the XR grab before destroying
        XRGrabInteractable grab = other.gameObject.GetComponent<XRGrabInteractable>();

        if (grab != null && grab.isSelected)
        {
            if (grab.firstInteractorSelecting != null)
            {
                grab.interactionManager.SelectExit(grab.firstInteractorSelecting, grab);
            }
        }
        yield return null;
        Destroy(other.gameObject);
    }
}
