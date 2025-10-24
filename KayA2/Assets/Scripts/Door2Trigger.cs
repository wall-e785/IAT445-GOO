using UnityEngine;

public class Door2Trigger : MonoBehaviour
{
    public float requiredPlayerScaleMin = 0.75f;//sets minimum scale for player to be to trigger door
    public float requiredPlayerScaleMax = 1.5f;//sets maximum scale for player to be to trigger door
    private Animator _Door2Animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Door2Animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)//when something collides with door collider
    {
        if(other.CompareTag("Player") && other.transform.localScale.y >= requiredPlayerScaleMin && other.transform.localScale.y <= requiredPlayerScaleMax)//if object colliding is player and is larger than the minimum y scale and maximum player y scale range then...
        {
            _Door2Animator.SetTrigger("open door 2");//open the door -> start the animation transition
        }
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)//when something exits the door collider
    {
        if(other.CompareTag("Player"))//if the soemthing is the player (has player tag)
        {
            _Door2Animator.SetTrigger("close door 2");//set door to close -> start animation transition
        }
    }
}
