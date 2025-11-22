using UnityEngine;

public class SizeCheckDoor : MonoBehaviour
{
    [SerializeField] private Animator doorControllerOne;
    [SerializeField] private Animator doorControllerTwo;
    private bool activated = false;
    private bool bigEnough = false;

    public void isBig()
    {
        bigEnough = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bigEnough && !activated)
        {
            //Debug.Log("door opening");
            AudioManager.Instance.PlaySound("Security Room Door Open");
            activated = true;
            doorControllerOne.SetBool("Opening", true);
            doorControllerTwo.SetBool("Opening", true);
        }
        else if(!bigEnough && !activated)
        {
            AudioManager.Instance.PlaySound("Not Tall");
        }
    }
}
