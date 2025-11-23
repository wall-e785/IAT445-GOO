using UnityEngine;
using System.Collections;

public class OfficeManager : MonoBehaviour
{

    private void Awake()
    {
        UIManager.Instance.setText("Escape the desk.");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlaySound("office-2.1");
        StartCoroutine(playDelay("office-2.2", 8.5f));
    }

    IEnumerator playDelay(string clipName, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySound(clipName);
    }
}
