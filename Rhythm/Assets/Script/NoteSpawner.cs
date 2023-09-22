using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoint;
    [SerializeField]
    GameObject note;

    [SerializeField]
    TXTManager t;

    string[] noteArr;
    private void Start()
    {
        t.ReadTextFile(out noteArr, t.path);
        StartCoroutine(SpawnNote());
    }

    IEnumerator SpawnNote()
    {
        // 6 = �������
        for (int i = 6; i < noteArr.Length; i++)
        {
            string[] tempLine = noteArr[i].Split(" ");
            for (int j = 1; j < tempLine.Length; j++)
            {
                if (tempLine[j].Contains("0"))
                {
                    //Instantiate(note, spawnPoint[j - 1]); // TODO:���� ������ƮǮ �ٲ��ֱ�
                }
                else
                {
                    Instantiate(note, spawnPoint[j - 1]); // TODO:���� ������ƮǮ �ٲ��ֱ�
                }
                
            }
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }

}
