using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Beam : MonoBehaviour
{
    public ControllerInputActionManager _input;//refernce the controller inputs
    public BoxScale interaction;//refernce the box interactions

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_input == null)
        {
            //if it cannot be found then look for the script
            _input = FindObjectOfType<ControllerInputActionManager>();
        }
        
    }
    //when something collides with lazerbeam collider
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Touched");
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
