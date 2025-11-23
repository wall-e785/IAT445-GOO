using UnityEngine;
using System.Collections;

public class CafManager : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlaySound("Drop");
        AudioManager.Instance.PlaySound("caf-4");
        UIManager.Instance.setText("Eat and grow. Avoid the CatBot.");
        StartCoroutine(playDelay("caf-1", 7));

    }

    IEnumerator playDelay(string clipName, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySound(clipName);
    }
}
