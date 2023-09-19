using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    [SerializeField]
    TXTManager t;
    [SerializeField]
    GameObject noteGroup;
    [SerializeField]
    GameObject contentUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            InitEditor(t.path);
        }
    }
    void InitEditor(string path)
    {
        t.ReadTextFile(out string[] lines, path);
        contentUI.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 20 * lines.Length);
        for (int i = lines.Length - 1; 0 <= i; i--)
        {
            GameObject tempNoteGroup = Instantiate(noteGroup);
            tempNoteGroup.transform.SetParent(contentUI.transform);
            tempNoteGroup.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 10);
        }
    }
}
