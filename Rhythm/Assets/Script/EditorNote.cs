using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorNote : MonoBehaviour
{
    [SerializeField]
    int index;
    bool isChecked = false;
    

    bool IsChecked
    {
        get { return isChecked; }
    }

    void Toggle()
    {
        isChecked = !isChecked;
    }
    public void OnClickNote()
    {
        Toggle();
    }
    // ���콺 �����̿����� �̺�Ʈ
}
