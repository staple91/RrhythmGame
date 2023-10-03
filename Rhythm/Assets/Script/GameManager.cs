using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int combo = 0;
    
    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        combo = 0;
        songInfo = new SongInfo();
    }
}
