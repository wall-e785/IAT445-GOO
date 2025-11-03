using UnityEngine;
using UnityEngine.Events;

//used for the ray on the air pump
public class PumpRay : MonoBehaviour
{
    public UnityEvent onTriggeredS1;
    public UnityEvent onTriggeredS2;
    public UnityEvent onTriggeredS3;
    public UnityEvent onTriggeredS4;


    public void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Screw1":
                onTriggeredS1?.Invoke();
                break;
            case "Screw2":
                onTriggeredS2?.Invoke();
                break;
            case "Screw3":
                onTriggeredS3?.Invoke();
                break;
            case "Screw4":
                onTriggeredS4?.Invoke();
                break;


        }
    }

}
