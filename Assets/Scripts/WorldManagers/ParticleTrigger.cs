using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    private bool triggered = false;
      private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !triggered)
        {
            AffordanceManager.Instance.progressParticles();
            triggered = true;
            Debug.Log("Particles triggered");
        }
    }
}
