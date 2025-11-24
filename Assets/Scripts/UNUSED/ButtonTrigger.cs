using UnityEngine;
using UnityEngine.Events;

//used for the forklift button
//referenced from https://www.youtube.com/watch?v=_pApJDiFxV4
public class ButtonTrigger : MonoBehaviour
{
    public Animator forkAnimator;
    private bool pressed = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button" && pressed == false)
        {
            forkAnimator.SetBool("Activated", true);
            AudioManager.Instance.PlaySound("License");
            pressed = true;
        }
    }
}
