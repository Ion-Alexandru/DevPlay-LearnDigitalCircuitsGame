using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager2 : MonoBehaviour
{
    public string targetSceneName;
    public float loadingDuration = 10f;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(loadingDuration);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneName);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}