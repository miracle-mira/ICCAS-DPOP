// SceneLoad 스크립트 (코드1)
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public Slider progressbar;
    public Text loadtext;
    public static string loadScene;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    public static void LoadSceneHandle(string name)
    {
        loadScene = name;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadSceneAsync()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            yield return null;
            if(progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value,0.9f,Time.deltaTime);
            }
            else if(progressbar.value >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value,1f,Time.deltaTime);
            }

            if (progressbar.value >= 1f)
            {
                loadtext.text = "Press SpaceBar";
            }

            if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && operation.progress>=0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }
    }
}
