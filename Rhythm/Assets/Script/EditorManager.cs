using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EditorManager : Singleton<EditorManager>
{
    [SerializeField]
    GameObject noteGroup;
    [SerializeField]
    GameObject contentUI;
    [SerializeField]
    GameObject divideUI;

    string currentPath;


    [SerializeField]
    TMP_InputField nameInput;
    [SerializeField]
    TMP_InputField bpmInput;
    [SerializeField]
    TMP_InputField timeInput;
    [SerializeField]
    TMP_InputField levelInput;

    [SerializeField]
    GameObject songInfoButtonObj;
    

    Queue<NoteGroup> noteGroupQueue = new Queue<NoteGroup>();




    public delegate void OnClickEditorButtonDel();

    public static Stack<OnClickEditorButtonDel> undoButtonDelStack = new Stack<OnClickEditorButtonDel>();
    public static Stack<OnClickEditorButtonDel> redoButtonDelStack = new Stack<OnClickEditorButtonDel>();



    public void EnableSongList()
    {
        foreach(SongInfo songInfo in TXTManager.Instance.songInfoData.songInfos)
        {
            Instantiate(songInfoButtonObj).TryGetComponent<SongInfoButton>(out SongInfoButton songInfoButton);
            songInfoButton.info = songInfo;
        }
        
    }

    public void LoadEditor(string path)
    {
        TXTManager.Instance.ReadTextFile(out string[] lines, path);
        currentPath = path;
        contentUI.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 20 * lines.Length + (5 * lines.Length) / 16); // 사이즈 10 top padding 5 bottonpadding 5
        for (int i = 0; i < lines.Length; i++)
        {
            if(lines[i].Length > 0)
            {

                if (lines[i][0] == '#')
                { 
                    NoteGroup tempNoteGroup = Instantiate(noteGroup).GetComponent<NoteGroup>();
                    if ((i - 6) % 16 == 0) // 6 = 헤더라인수
                        Instantiate(divideUI).transform.SetParent(contentUI.transform);
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
                }
            }
        }
    }

    public void CreateFile()
    {
        TXTManager.Instance.GenerateFile(nameInput.text, bpmInput.text, levelInput.text, timeInput.text);
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
        TXTManager.Instance.ModifyText(currentPath, resultStringList.ToArray());
    }

    public void UndoNoteClick()
    {
        if (undoButtonDelStack.Count > 0)
        {
            redoButtonDelStack.Push(undoButtonDelStack.Peek());
            undoButtonDelStack.Pop()();
        }
    }
    public void RedoNoteClick()
    {
        if(redoButtonDelStack.Count > 0)
        {
            undoButtonDelStack.Push(redoButtonDelStack.Peek());
            redoButtonDelStack.Pop()();
        }
    }
}
