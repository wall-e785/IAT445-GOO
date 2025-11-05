using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class SoundTrigger : MonoBehaviour
{

    public AudioClip clip;
    private bool played = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !played)
        {
            AudioManager.Instance.PlaySound(clip.name);
            played = true;
        }
    }
}
