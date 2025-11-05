using UnityEngine;
using UnityEngine.Events;
public class LevelTrigger : MonoBehaviour
{
    public UnityEvent onTriggered;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            onTriggered?.Invoke();
        }
    }

}