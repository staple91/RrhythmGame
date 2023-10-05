using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    string jsonFilePath = (Application.streamingAssetsPath + @"\" + "JsonFile" + ".txt");

    public List<SongInfo> songInfoList;

    public int combo = 0;
    
    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    void Init()
    {
        DontDestroyOnLoad(gameObject);
        combo = 0;
        songInfo = new SongInfo();
        ReadJason();
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
