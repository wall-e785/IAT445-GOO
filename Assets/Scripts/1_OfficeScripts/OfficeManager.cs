using UnityEngine;
using System.Collections;

public class OfficeManager : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.setText("Find a way to escape through the vent. Use the ‘A’ button to jump.");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlaySound("office-2.1");
        StartCoroutine(playDelay("office-2.2", 7f));
    }

    IEnumerator playDelay(string clipName, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySound(clipName);
    }

}
