using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using SimpleFileBrowser;
public class SelectManager : MonoBehaviour
{
    [SerializeField]
    Canvas newSongCanvas;
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

    public void CreateNewSong()
    {
        newSongCanvas.gameObject.SetActive(true);
    }

    public void CreateFile()
    {
        TXTManager.Instance.GenerateFile(nameInput.text, bpmInput.text, levelInput.text, timeInput.text);
    }
    public void OpenBrowser()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("music", ".mp3"));
        FileBrowser.SetDefaultFilter(".jpg");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        if (FileBrowser.Success)
        {
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

            string destinationPath = Application.dataPath + @"\Resources\" + nameInput.text + ".mp3";
            FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
            CreateFile();
        }
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
