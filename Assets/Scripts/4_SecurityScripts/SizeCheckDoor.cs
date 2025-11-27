using UnityEngine;
using System.Collections;

public class SizeCheckDoor : MonoBehaviour
{
    [SerializeField] private Animator doorControllerOne;
    [SerializeField] private Animator doorControllerTwo;
    private bool activated = false;
    private bool bigEnough = false;
    private bool thoughtPlaying = false;

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
            UIManager.Instance.setText("Find the Mira-Goo boxes to reach the Pink Keycard, escape with the Lab Shuttle!");
        }
        else if(!bigEnough && !activated)
        {
            AudioManager.Instance.PlaySound("Not Tall");

            if (!thoughtPlaying)
            {
                thoughtPlaying = true;
                StartCoroutine(showThought());
            }
        }
    }

    IEnumerator showThought()
    {
        UIManager.Instance.setThought("Goo short... :c");
        yield return new WaitForSeconds(3);
        UIManager.Instance.setThought("Goo + Food = Tall... :D");
        yield return new WaitForSeconds(3);
    }
}
