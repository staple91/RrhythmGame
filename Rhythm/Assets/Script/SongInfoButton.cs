using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongInfoButton : MonoBehaviour
{
    public SongInfo info;
    
    public void SelectOnEditor()
    {
        EditorManager.Instance.LoadEditor(info.path);
    }
}
