using UnityEngine;
using System.Collections;

public class CafManager : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlaySound("Drop");
        AudioManager.Instance.PlaySound("caf-4");
        UIManager.Instance.setText("Avoid the CatBot. Move the obstacles to reach the garbage chute.");
        StartCoroutine(playDelay("caf-1", 7));

    }

    IEnumerator playDelay(string clipName, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySound(clipName);
    }
}
