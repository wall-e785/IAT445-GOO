using UnityEngine;
using System.Collections;

public class CafManager : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlaySound("Drop");
        UIManager.Instance.setText("Eat, then find the escape route. Be careful to avoid the CatBot.");
        StartCoroutine(playDelay("caf-1", 7));

    }

    IEnumerator playDelay(string clipName, float delay)
    {
        yield return new WaitForSeconds(.5f);
        AudioManager.Instance.PlaySound("caf-4");
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySound(clipName);
    }
}
