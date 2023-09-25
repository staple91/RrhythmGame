using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


enum InfoType
{
    Name,
    Bpm,
    Level,
    Time
}
public class TXTManager : MonoBehaviour
{
    string fileName = @"\asd.txt";
    public string path;
    Dictionary<string, InfoType> dic = new Dictionary<string, InfoType>();
    StreamReader reader;

    // Start is called before the first frame update
    void Start()
    {
        dic.Add("Name", InfoType.Name);
        dic.Add("Bpm", InfoType.Bpm);
        dic.Add("Level", InfoType.Level);
        dic.Add("Time", InfoType.Time);

        path = Application.streamingAssetsPath + fileName;
        
        GenerateFile("asd", "dlfma", int.Parse("100"), "2" , int.Parse("30"));
        ReadTextFile(out var test, path);
        ReadHeader(test);
    }

    void GenerateFile(string fileName, string name, float bpm, string level, float time)
    {
        FileInfo fi = new FileInfo(Application.streamingAssetsPath + @"\" + fileName + ".txt");
        StreamWriter writer;
        if (!fi.Exists)
        {
            writer = new StreamWriter(fi.Create());
        }
        else
        {
            Debug.LogError("파일이 이미 존재함");
            return;
        }
        writer.WriteLine("HEADER");
        writer.WriteLine("Name:" + name);
        writer.WriteLine("Bpm:" + bpm);
        writer.WriteLine("Level:" + level);
        writer.WriteLine("Time:" + time);
        writer.WriteLine("ENDHEADER");
        for (int i = 0; i < bpm / 60 * time; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                if (i == (bpm / 60 * time - 1) && j == 15)
                    writer.Write("#" + i.ToString("D3") + j.ToString("D2") + " 0 0 0 0 1");
                else
                    writer.WriteLine("#" + i.ToString("D3") + j.ToString("D2") + " 0 0 0 0 1");
            }
        }
        writer.Close();
    }
    // 
    public void ReadTextFile(out string[] strings , string path)
    {
        StreamReader streamReader = new StreamReader(path);
        if (streamReader == null)
        {
            strings = null;
            return;
        }
        string tempString = streamReader.ReadToEnd();
        strings = tempString.Split('\n');
        for (int i = 0; i < strings.Length - 1  ; i++) // 마지막줄은 writeLine을 사용하지 않기 때문에 마지막줄 전 까지만 끝문자를 잘라줌.
        {
            strings[i] = strings[i].Substring(0, strings[i].Length - 1);
        }
        streamReader.Close();
    }

    public void ModifyText(string path, string[] values)
    {

        ReadTextFile(out string[] lines , path);
        
        StreamWriter streamWriter = new StreamWriter(path, false); 
        if (streamWriter == StreamWriter.Null)
            return;
        for(int i = 0; i < values.Length; i++)
        {
            lines[i + 6] = values[i];
        }
        foreach(string line in lines)
        {
            if (line != lines[lines.Length - 1])
                streamWriter.WriteLine(line);
            else
                streamWriter.Write(line);
        }
        streamWriter.Close();
    }
    string[] ReadHeader(string[] lines)
    {
        List<string> headerLines = new List<string>();
        foreach (string line in lines)
        {
            if (line.Length > 0)
            {
                headerLines.Add(line);
                if (line.Equals("ENDHEADER"))
                {
                    Debug.Log("endOfHeader");
                    SaveHeader(headerLines.ToArray());
                    return headerLines.ToArray();
                }
                if (line.Equals("HEADER"))
                {
                    Debug.Log("readingHeader");
                }

                

            }
        }
        return null;
    }
    // SaveHeader(ReadHeader(string[]))
    void SaveHeader(string[] headerLines)
    {
        SongInfo tempSongInfo = new SongInfo();
        foreach (string line in headerLines)
        {
            string[] tempString = line.Split(':');
            if (dic.TryGetValue(tempString[0], out var infoType))
            {
                SaveInfo(ref tempSongInfo, infoType, tempString[1]);
            }
        }
        GameManager.Instance.songInfo = tempSongInfo;
    }
   
    void SaveInfo(ref SongInfo songInfo, InfoType infoType, string value)
    {
        switch(infoType)
        {
            case InfoType.Name:
                songInfo.name = value;
                break;
            case InfoType.Bpm:
                songInfo.bpm = int.Parse(value);
                break;
            case InfoType.Level:
                songInfo.level = int.Parse(value);
                break;
            case InfoType.Time:
                songInfo.time = int.Parse(value);
                break;
        }
    }
}
