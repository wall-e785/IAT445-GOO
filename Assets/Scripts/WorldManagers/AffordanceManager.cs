using UnityEngine;

public class AffordanceManager : MonoBehaviour
{
    public static AffordanceManager Instance;
    public ParticleSystem[] ParticleSystems;
    private int pos = 0;
    private ParticleSystem curr;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (ParticleSystems[pos] != null)
        {
            curr = ParticleSystems[pos];
            curr.Play();
        }
    }

    public void progressParticles()
    {
        if(pos < ParticleSystems.Length)
        {
            curr.Stop();
            pos++;
            if(pos < ParticleSystems.Length)
            {
                curr = ParticleSystems[pos];
                curr.Play();
            }
        }
    }
}
