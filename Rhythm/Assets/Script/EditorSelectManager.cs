using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EditorSelectManager : MonoBehaviour
{
    [Header("Input Fields")]
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


    public void CreateFile()
    {
        TXTManager.Instance.GenerateFile(nameInput.text, bpmInput.text, levelInput.text, timeInput.text);
    }

    public void EnableSongList()
    {
        foreach (SongInfo songInfo in GameManager.Instance.songInfoList)
        {
            Instantiate(songInfoButtonObj).TryGetComponent<SongInfoButton>(out SongInfoButton songInfoButton);
            songInfoButton.info = songInfo;
        }
    }
    
}
