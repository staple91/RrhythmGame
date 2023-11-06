using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EditorManager : Singleton<EditorManager>
{
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    GameObject noteGroup;
    [SerializeField]
    GameObject contentUI;
    [SerializeField]
    GameObject divideUI;
    [SerializeField]
    Scrollbar scroll;

    string currentPath;

    Coroutine playSongCo;

    

    Queue<NoteGroup> noteGroupQueue = new Queue<NoteGroup>();




    public delegate void OnClickEditorButtonDel();

    public static Stack<OnClickEditorButtonDel> undoButtonDelStack = new Stack<OnClickEditorButtonDel>();
    public static Stack<OnClickEditorButtonDel> redoButtonDelStack = new Stack<OnClickEditorButtonDel>();


    private void Start()
    {
        LoadEditor(PlayerPrefs.GetString("Path"));
    }

    public void StartSong()
    {
        audio.time = scroll.value * audio.clip.length;
        audio.Play();
        playSongCo = StartCoroutine(PlaySongCo());
    }
    public void StopSong()
    {
        if(playSongCo != null)
            StopCoroutine(playSongCo);
        audio.Stop();
    }
    IEnumerator PlaySongCo()
    {
        while(true)
        {
            scroll.value = audio.time / audio.clip.length;
            yield return null;
        }
    }
    public void LoadEditor(string path)
    {
        TXTManager.Instance.ReadTextFile(out string[] lines, path);
        currentPath = path;
        contentUI.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 20 * lines.Length + (5 * lines.Length) / 16); // 사이즈 10 top padding 5 bottonpadding 5
        for (int i = lines.Length - 1; i >= 0; i--)
        {
            if(lines[i].Length > 0)
            {

                if (lines[i][0] == '#')
                { 
                    NoteGroup tempNoteGroup = Instantiate(noteGroup).GetComponent<NoteGroup>();
                    if ((i - 5) % 16 == 0) // 6 = 헤더라인수
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

    

    public void SaveLines()
    {
        List<string> resultStringList = new List<string>();
        int index = 0;
        foreach(NoteGroup noteGroup in noteGroupQueue)
        {
            string tempString = "#" + (index / 16).ToString("D3") + (index % 16).ToString("D2") ;
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
