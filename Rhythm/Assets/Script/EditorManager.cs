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

    Queue<NoteGroup> noteGroupQueue = new Queue<NoteGroup>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            LoadEditor(t.path);
        }
    }
    void LoadEditor(string path)
    {
        t.ReadTextFile(out string[] lines, path);

        contentUI.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 20 * lines.Length);
        for (int i = 0; i < lines.Length; i++)
        {
            if(lines[i].Length > 0)
            {

                if (lines[i][0] == '#')
                {
                    NoteGroup tempNoteGroup = Instantiate(noteGroup).GetComponent<NoteGroup>();
                    string[] tempLine = lines[i].Split(" ");
                    for (int j = 0; j < tempNoteGroup.notes.Length; j++)
                    {

                        if (tempLine[j + 1].Contains("0"))
                        {
                            tempNoteGroup.notes[j].isChecked = false;
                        }
                        else
                        {
                            tempNoteGroup.notes[j].isChecked = true;
                        }

                    }
                    noteGroupQueue.Enqueue(tempNoteGroup);

                    tempNoteGroup.gameObject.transform.SetParent(contentUI.transform);
                    tempNoteGroup.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 10);
                }
            }
        }
    }

    public void SaveLines()
    {
        List<string> resultStringList = new List<string>();
        int index = 0;
        foreach(NoteGroup noteGroup in noteGroupQueue)
        {
            string tempString = "#"+(index / 16).ToString("D3") + (index % 16).ToString("D2") ;
            for (int i = 0; i < noteGroup.notes.Length; i++)
            {
                if(noteGroup.notes[i].isChecked)
                {
                    tempString += " 1";
                }
                else
                {
                    tempString += " 0";
                }
            }
            resultStringList.Add(tempString);
            index++;
        }
        t.ModifyText(t.path, resultStringList.ToArray());
    }
}
