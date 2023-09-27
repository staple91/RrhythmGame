using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponent : MonoBehaviour, ITogglalbe
{
    public void Off()
    {
        gameObject.SetActive(false);
        if(UIManager.Instance.uiList.Contains(gameObject))
            UIManager.Instance.uiList.Remove(gameObject);
    }

    public void Pop()
    {
        gameObject.SetActive(true);
        UIManager.Instance.uiList.Add(gameObject);
    }

}
