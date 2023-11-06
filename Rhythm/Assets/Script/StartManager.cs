using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void SelectScene(string sceneName)
    {
        LoadingHelper.Instance.targetSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}
