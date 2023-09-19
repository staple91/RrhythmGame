using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorNote : MonoBehaviour
{
    
    [SerializeField]
    int index;
    bool isChecked = false;

    public bool IsChecked
    {
        get { return isChecked; }
    }

    RawImage image;

    private void Start()
    {
        image = GetComponent<RawImage>();
        image.color = new Color(1, 1, 1, 0.3f);
    }

    void Toggle()
    {
        isChecked = !isChecked;
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
