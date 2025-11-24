using UnityEngine;

public class SizeCheckDoor : MonoBehaviour
{
    [SerializeField] private Animator doorControllerOne;
    [SerializeField] private Animator doorControllerTwo;
    private bool activated = false;
    private bool bigEnough = false;

    public void isBig(bool val)
    {
        bigEnough = val;
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
            UIManager.Instance.setText("Find the Blue Keycard to open the door.");
            UIManager.Instance.setThought("Need... Card...");
        }
        else if(!bigEnough && !activated)
        {
            AudioManager.Instance.PlaySound("Not Tall");
            UIManager.Instance.setThought("Goo + Food = Tall...");
        }
    }
}
