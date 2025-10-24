using UnityEngine;
using UnityEngine.UI;

//manages the home/tutorial scene
public class TutorialManager : MonoBehaviour
{
    public Button startButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        AudioManager.Instance.PlaySound("tutorial-1");
    }
}
