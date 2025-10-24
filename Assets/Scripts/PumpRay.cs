using UnityEngine;
using UnityEngine.Events;

//used for the ray on the air pump
public class PumpRay : MonoBehaviour
{
    public UnityEvent onTriggered;

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Goo")
        {
            Debug.Log("Raycasted Goo");
            onTriggered?.Invoke();
        }
    }

}
