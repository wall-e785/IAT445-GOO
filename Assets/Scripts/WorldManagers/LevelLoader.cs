using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1;
    public static LevelLoader instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    LoadNextLevel();
        //}
    }

    public void LoadNextLevel()
    {
        int currLevel = SceneManager.GetActiveScene().buildIndex;
        if (currLevel < 4)
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        instance.transition.SetBool("StartTransition", true);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
        yield return new WaitForSeconds(2);
        instance.transition.SetBool("StartTransition", false);
    }
}
