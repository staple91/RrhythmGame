using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingHelper : Singleton<LoadingHelper>
{
    public string targetSceneName;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
