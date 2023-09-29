using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoint;
    [SerializeField]
    Transform[] dirPoint;
    [SerializeField]
    GameObject note;

    [SerializeField]
    TXTManager t;


    public static List<Queue<Note>> noteQueueList = new List<Queue<Note>>();

    string[] noteArr;

    private void Start()
    {
        t.ReadTextFile(out noteArr, t.path);
        for (int i = 0; i < 5; i++) // 5 = 라인수
        {
            noteQueueList.Add(new Queue<Note>());
        }
        StartCoroutine(SpawnNote());
    }

    IEnumerator SpawnNote()
    {
        Debug.Log(5 - (dirPoint[0].position - spawnPoint[0].position).magnitude / note.GetComponent<Note>().speed);
        yield return new WaitForSeconds(5 - (dirPoint[0].position - spawnPoint[0].position).magnitude / note.GetComponent<Note>().speed);
        // 6 = 헤더길이
        for (int i = 6; i < noteArr.Length; i++)
        {
            string[] tempLine = noteArr[i].Split(" ");
            for (int j = 1; j < tempLine.Length; j++)
            {
                if (tempLine[j].Contains("0"))
                {
                    //Instantiate(note, spawnPoint[j - 1]); // TODO:추후 오브젝트풀 바꿔주기
                }
                else
                {
                    noteQueueList[j - 1].Enqueue(Instantiate(note, spawnPoint[j - 1]).GetComponent<Note>()); // TODO:추후 오브젝트풀 바꿔주기
                }
                
            }
            Debug.Log(GameManager.Instance.songInfo.bpm);
            Debug.Log(GameManager.Instance.songInfo.time);
            yield return new WaitForSeconds((float)60 / (GameManager.Instance.songInfo.bpm*16));
        }
        yield return null;
    }

}
