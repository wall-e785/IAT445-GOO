using UnityEngine;

public class TextTrigger : MonoBehaviour
{

    [TextArea] public string text;
    private bool changed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !changed)
        {
            UIManager.Instance.setText(text);
            changed = true;
        }
    }
}
