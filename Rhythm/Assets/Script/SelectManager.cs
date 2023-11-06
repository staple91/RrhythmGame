using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SelectManager : MonoBehaviour
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
    [SerializeField]
    Transform canvasTr;

    private void Start()
    {
        EnableSongList();
    }
    public void CreateFile()
    {
        TXTManager.Instance.GenerateFile(nameInput.text, bpmInput.text, levelInput.text, timeInput.text);
    }

    public void EnableSongList()
    {
        foreach (SongInfo songInfo in GameManager.Instance.songInfoList)
        {
            GameObject buttonObj = Instantiate(songInfoButtonObj);
            buttonObj.TryGetComponent<SongInfoButton>(out SongInfoButton songInfoButton);
            buttonObj.transform.SetParent(canvasTr);
            songInfoButton.info = songInfo;

        }
    }
    
}
