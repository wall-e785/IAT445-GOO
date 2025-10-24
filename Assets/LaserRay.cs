using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class LaserRay : MonoBehaviour
{
    public ControllerInputActionManager _input;//reference the controller inputs
    public BoxScale interaction;//reference the box interactions
    public UnityEvent onTriggered;
    public float rayLength = 2f; // How far the ray should reach

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))//extend raycast out
        {
            if (hit.collider.CompareTag("Objects"))//if raycast collides with object that has tag objects
            {
                Debug.Log("Raycasted");

                onTriggered?.Invoke();
                //if player presses shrink input key
                if (_input.Shrink())
                {
                    Debug.Log("Shrunk");
                    interaction.BeginShrink();
                }
                //if player presses grow input key
                if (_input.Grow())
                {
                    Debug.Log("Grew");
                    interaction.BeginGrow();
                } 
            }
        }

    }
    
    //create visualizer for the raycast
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * rayLength);//draw a green line directly infront of it at the raycast length
    }
}
