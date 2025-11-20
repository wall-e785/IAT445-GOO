using UnityEngine;
using System.Collections;

public class PetriManager : MonoBehaviour
{
    public GameObject goo1;
    public GameObject goo2;
    public GameObject goo3;

    private bool introPlaying = true;
    private bool done = false;

    private void Awake()
    {
        AudioManager.Instance.PlaySound("office-1");
        UIManager.Instance.setText("Eat and grow. Grab food and drag it towards yourself to eat.");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(waitForIntro());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("checking");
        if (goo1 == null && goo2 == null && goo3 == null && !done && !introPlaying)
        {
            done = true;
            AudioManager.Instance.PlaySound("office-2.1");
            StartCoroutine(playDelay("office-2.2", 8.5f));
        }

    }

    IEnumerator waitForIntro()
    {
        yield return new WaitForSeconds(24);
        UIManager.Instance.setText("Wait for the right moment to escape... It is watching.");
        introPlaying = false;
    }

    IEnumerator playDelay(string clipName, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySound("BurpSmall");
        AudioManager.Instance.PlaySound(clipName);
        yield return new WaitForSeconds(5);
        LevelLoader.instance.LoadNextLevel();

    }

}
