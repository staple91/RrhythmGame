using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongInfoButton : MonoBehaviour
{
    public SongInfo info;
    
    public void SelectOnEditor()
    {
        PlayerPrefs.SetString("EditPath", info.path);
        LoadEditScene();
    }
    public void LoadEditScene()
    {
        LoadingHelper.Instance.targetSceneName = "EditorScene";
        SceneManager.LoadScene("LoadingScene");
    }
}
