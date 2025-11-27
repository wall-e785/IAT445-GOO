using UnityEngine;
using UnityEngine.Events;

//used for the ray on the air pump
public class PumpRay : MonoBehaviour
{
    //screws
    public UnityEvent onTriggeredS1;
    public UnityEvent onTriggeredS2;
    public UnityEvent onTriggeredS3;
    public UnityEvent onTriggeredS4;

    //grow boxes
    public UnityEvent onTriggeredB1;
    public UnityEvent onTriggeredB2;
    public UnityEvent onTriggeredB3;
    public UnityEvent onTriggeredB4;
    public UnityEvent onTriggeredB5;

    private void OnTriggerEnter(Collider other)
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
            case "B1":
                AudioManager.Instance.PlaySound("Goo Box Grow");
                onTriggeredB1?.Invoke();
                break;
            case "B2":
                AudioManager.Instance.PlaySound("Goo Box Grow");
                onTriggeredB2?.Invoke();
                break;
            case "B3":
                AudioManager.Instance.PlaySound("Goo Box Grow");
                onTriggeredB3?.Invoke();
                break;
            case "B4":
                AudioManager.Instance.PlaySound("Goo Box Grow");
                onTriggeredB4?.Invoke();
                break;
            case "B5":
                AudioManager.Instance.PlaySound("Goo Box Grow");
                onTriggeredB5?.Invoke();
                break;

        }
    }
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
            case "B1":
                onTriggeredB1?.Invoke();
                break;
            case "B2":
                onTriggeredB2?.Invoke();
                break;
            case "B3":
                onTriggeredB3?.Invoke();
                break;
            case "B4":
                onTriggeredB4?.Invoke();
                break;
            case "B5":
                onTriggeredB5?.Invoke();
                break;

        }
    }

}
