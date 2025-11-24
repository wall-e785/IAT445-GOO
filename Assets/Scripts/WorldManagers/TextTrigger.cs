using UnityEngine;

public class TextTrigger : MonoBehaviour
{

    [TextArea] public string text;
    [TextArea] public string thought;
    private bool changed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !changed)
        {
            if(text != null) UIManager.Instance.setText(text);
            if(thought != null) UIManager.Instance.setThought(thought);
            changed = true;
        }
    }
}
