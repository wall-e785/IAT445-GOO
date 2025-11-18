using UnityEngine;

public class OfficeManager : MonoBehaviour
{

    public GameObject goo1;
    public GameObject goo2;
    public GameObject goo3;

    private bool done = false;

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
        Debug.Log("checking");
        if(goo1 == null && goo2 == null && goo3 == null && !done)
        {
            done = true;
            LevelLoader.instance.LoadNextLevel();
        }
    }
}
