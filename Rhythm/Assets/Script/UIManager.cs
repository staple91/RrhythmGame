using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITogglalbe
{
    void Pop();
    void Off();
}

public class UIManager : Singleton<UIManager>
{
    public List<GameObject> uiList = new List<GameObject>();
    protected override void Awake()
    {
        base.Awake();
    }
}
