using UnityEngine;

public class GooBoxSizeCheck : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.gameObject.tag;
        if(otherTag == "B1" || otherTag == "B2" || otherTag == "B3" || otherTag == "B4" || other.tag == "B5")
        {
            if (!other.GetComponent<GooGrow>().done)
            {
                AudioManager.Instance.PlaySound("Negative");
                UIManager.Instance.setThought("Box no fit");
            }
        }
    }
}
