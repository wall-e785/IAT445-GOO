using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PickUpManager : MonoBehaviour
{
    private string tagName;
    private string clipName;
    private XRGrabInteractable grab;
    public ParticleSystem affordance;
    

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();

        if(grab != null)
        {
            grab.selectEntered.AddListener(OnPickedUp);
            grab.selectExited.AddListener(OnReleased);
        }

        //Debug.Log(grab);
    }

    void Start()
    {
        tagName = gameObject.tag;

        switch (tagName)
        {
            case "SmallFood":
            case "WarehouseKey":
            case "BlueKey":
            case "PinkKey":
            case "GooDrop":
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
            case "B3":
            case "B4":
            case "B5":
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
        }

        Debug.Log(tagName);
    }

    private void OnPickedUp(SelectEnterEventArgs args)
    {
        Debug.Log("playing sound");
        AudioManager.Instance.PlaySound(clipName);

        if(affordance != null)
        {
            affordance.Stop();
            affordance.Clear();
        }

    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if(affordance != null)
        {
            affordance.Play();
        }
    }
}
