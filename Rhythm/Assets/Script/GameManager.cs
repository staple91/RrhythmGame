using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;

[Serializable]
public struct SongInfo 
{
    public string name;
    public int bpm;
    public int level;
    public int time;
    public string path;
}

public class GameManager : Singleton<GameManager>
{
    public SongInfo songInfo;


    string jsonFilePath;

    public List<SongInfo> songInfoList;

    public int combo = 0;
    
    protected override void Awake()
    {
        base.Awake();
        Init();
        songInfo = new SongInfo();
        SceneManager.sceneLoaded += Init;
        DontDestroyOnLoad(gameObject);
    }
    void Init()
    {
        jsonFilePath = (Application.dataPath + @"\Resources\" + "JsonFile" + ".txt");
        combo = 0;
        ReadJason();
    }
    void Init(Scene scnen, LoadSceneMode mode)
    {
        Init();
    }
    void ReadJason()
    {
        using (StreamReader reader = new StreamReader(jsonFilePath))
        {
            songInfoList = JsonConvert.DeserializeObject<List<SongInfo>>(reader.ReadToEnd());
        }
        if(songInfoList == null)
            songInfoList = new List<SongInfo>();
    }
    public void RefreshSongList()
    {
        StreamWriter jsonWriter = new StreamWriter(jsonFilePath);
        jsonWriter.Write(JsonConvert.SerializeObject(songInfoList));
        jsonWriter.Close();
        ReadJason();
    }

}
