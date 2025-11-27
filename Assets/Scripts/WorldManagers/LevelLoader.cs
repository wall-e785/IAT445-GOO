using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //public Animator transition;
    public float transitionTime = 1;
    public static LevelLoader instance;
    public FadeScript fader;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        fader.FadeIn();
        //StartCoroutine(DisableQuad());
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            StartCoroutine(CreditsTimer());
        }
    }

    public void LoadNextLevel()
    {
        fader.gameObject.SetActive(true);
        int currLevel = SceneManager.GetActiveScene().buildIndex;
        if (currLevel < 5)
        {
            if(currLevel == 4)
            {
                fader.fadeColor = Color.white;
            }
            StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            StartCoroutine(LoadLevelAsync(0));
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //instance.transition.SetBool("StartTransition", true);
        fader.FadeOut();
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
        //yield return new WaitForSeconds(2);
        //instance.transition.SetBool("StartTransition", false);
    }

    IEnumerator DisableQuad()
    {
        float timer = 0;
        while (timer <= fader.fadeDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        fader.gameObject.SetActive(false);
    }

    IEnumerator LoadLevelAsync(int levelIndex)
    {
        fader.FadeOut();
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
        operation.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fader.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            yield return new WaitForSeconds(3);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            yield return new WaitForSeconds(4);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }

        operation.allowSceneActivation = true;

    }

    IEnumerator CreditsTimer()
    {
        yield return new WaitForSeconds(32);
        LoadNextLevel();
    }
}
