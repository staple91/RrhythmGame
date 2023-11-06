using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongInfoButton : MonoBehaviour
{
    public SongInfo info;
    
    public void SelectSong()
    {
        GameManager.Instance.songInfo = info;
        PlayerPrefs.SetString("Path", info.path);
        LoadScene();
    }
    public void LoadScene()
    {
        if(LoadingHelper.Instance.targetSceneName == "EditorSelectScene")
        {   
            LoadingHelper.Instance.targetSceneName = "EditorScene";
            SceneManager.LoadScene("LoadingScene");
        }
        else if (LoadingHelper.Instance.targetSceneName == "GameSelectScene")
        {
            LoadingHelper.Instance.targetSceneName = "GameScene";
            SceneManager.LoadScene("LoadingScene"); 
        }
    }
}
