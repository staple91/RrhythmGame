using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorNote : MonoBehaviour
{
    [SerializeField]
    int index;


    [SerializeField]
    RawImage image;

    public bool isChecked = false;

    private void Start()
    {
        image.color = new Color(1, 1, 1, 0.3f);
        SetColor();
    }

    void Toggle()
    {
        isChecked = !isChecked;
        SetColor();
    }

    public void SetColor()
    {
        if (isChecked)
        {
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            image.color = new Color(1, 1, 1, 0.3f);
        }
    }
    public void OnClickNote()
    {
        EditorManager.redoButtonDelStack.Clear();
        EditorManager.undoButtonDelStack.Push(this.Toggle);
        
        Toggle();
    }
}
