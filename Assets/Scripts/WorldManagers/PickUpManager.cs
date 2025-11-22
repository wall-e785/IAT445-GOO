using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PickUpManager : MonoBehaviour
{
    string tag;
    string clipName;
    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnPickedUp);
        //Debug.Log(grab);
    }

    void Start()
    {
        tag = gameObject.tag;

        switch (tag)
        {
            case "SmallFood":
            case "WarehouseKey":
            case "BlueKey":
            case "PinkKey":
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    clipName = "PUSmallest";
                }
                else
                {
                    clipName = "PUSmall";
                }
                break;
            case "MediumFood":
            case "RegularPU":
                clipName = "PUMedium";
                break;
            case "LargeFood":
            case "B1":
            case "B2":
                clipName = "PULarge";
                break;
            case "LargePU":
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    clipName = "PULargest";
                }
                else
                {
                    clipName = "PULarge";
                }
                break;
            case "B3":
            case "B4":
            case "B5":
                clipName = "PULargest";
                break;
        }

        Debug.Log(tag);
    }

    private void OnPickedUp(SelectEnterEventArgs args)
    {
        Debug.Log("playing sound");
        AudioManager.Instance.PlaySound(clipName);
    }
}
