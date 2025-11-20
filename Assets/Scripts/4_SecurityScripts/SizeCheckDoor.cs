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
            activated = true;
            doorControllerOne.SetBool("Opening", true);
            doorControllerTwo.SetBool("Opening", true);
        }
    }
}
