using TMPro;
using UnityEngine;

//used to control the task UI and other UI that needs to appear within the player's view at all times
//heavily referenced from AudioManager, each Instance is per scene.
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI taskText;
    public TextMeshProUGUI gooThoughts;

    private void Awake()
    {
        Instance = this;
    }

    public void setText(string text)
    {
        taskText.text = text;
    }

    public void setThought(string text)
    {
        gooThoughts.text = text;
    }

}
