using UnityEngine;
using UnityEngine.Events;

//used for the forklift button
//referenced from https://www.youtube.com/watch?v=_pApJDiFxV4
public class ButtonTrigger : MonoBehaviour
{
    private bool pressed = false;
    public UnityEvent onPressed;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button" && pressed == false)
        {
            Debug.Log("I have been pressed");
            pressed = true;
            onPressed?.Invoke();
        }
    }
}
