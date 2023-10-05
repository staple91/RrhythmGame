using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingComponent : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadSceneCo());
    }
    IEnumerator LoadSceneCo()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LoadingHelper.Instance.targetSceneName);
        asyncLoad.allowSceneActivation = false;
        while(!asyncLoad.isDone)
        {
            if(asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
                break;
            }
            yield return null;
        }
    }
}
