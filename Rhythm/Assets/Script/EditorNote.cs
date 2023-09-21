using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorNote : MonoBehaviour
{
    
    [SerializeField]
    int index;

    public bool isChecked = false;

    [SerializeField]
    RawImage image;

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
        Toggle();
    }
}
