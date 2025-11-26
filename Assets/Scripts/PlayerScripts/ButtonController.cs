
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;


public class ButtonController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private InputDevice leftController;
    private InputDevice rightController;

    void Start()
    {
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (leftController == null || rightController == null)
        {
            Debug.Log("couldn't find controllers");
        }
        Debug.Log(leftController.isValid);
        Debug.Log(rightController.isValid);
    }

    void Update()
    {
        // LEFT HAND (X / Y)
        if (leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool xPressed) && xPressed)
            Debug.Log("X button pressed");

        if (leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool yPressed) && yPressed)
            Debug.Log("Y button pressed");

        // RIGHT HAND (A / B)
        if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool aPressed) && aPressed)
            Debug.Log("A button pressed");

        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool bPressed) && bPressed)
            Debug.Log("B button pressed");

        if (!leftController.isValid)
            leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (!rightController.isValid)
            rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

    }
}
