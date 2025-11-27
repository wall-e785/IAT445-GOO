using UnityEngine;

public class GooBoxSizeCheck : MonoBehaviour
{

    public GooGrow box1;
    public GooGrow box2;
    public GooGrow box3;

    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.gameObject.tag;
        if(otherTag == "B1" || otherTag == "B2" || otherTag == "B3")
        {
            if (!other.GetComponent<GooGrow>().done)
            {
                AudioManager.Instance.PlaySound("Negative");
                UIManager.Instance.setThought("Box no fit");
            }
        }
    }
}
