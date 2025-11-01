using UnityEngine;

public class OfficeManager : MonoBehaviour
{
    public AudioClip office1;
    public AudioClip office2_1;
    public AudioClip office2_2;
    public AudioClip office3;


    private void Awake()
    {
        AudioManager.Instance.PlaySound("office-1");
        UIManager.Instance.setText("Eat and grow.");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
