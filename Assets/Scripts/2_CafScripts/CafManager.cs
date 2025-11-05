using UnityEngine;
using System.Collections;

public class CafManager : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlaySound("caf-1");
        UIManager.Instance.setText("Eat and grow. Avoid the CatBot.");
        StartCoroutine(playDelay("caf-2", 1.2f));

    }

    IEnumerator playDelay(string clipName, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySound(clipName);
    }
}
