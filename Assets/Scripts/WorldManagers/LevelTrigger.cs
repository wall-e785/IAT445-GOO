using UnityEngine;
using UnityEngine.Events;
public class LevelTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelLoader.instance.LoadNextLevel();
        }
    }

}