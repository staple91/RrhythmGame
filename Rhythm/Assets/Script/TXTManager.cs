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

        path = Application.dataPath + fileName;
        //string testString = new StreamReader(path).ReadToEnd();
        //string[] testArr = testString.Split('\n');
        
        //ReadHeader(testArr);
        GenerateFile("asd", "dlfma", int.Parse("100"), "2" , int.Parse("60"));
        ModifyText(path, 5, 3, new int[5] { 2, 0, 5, 0, 0 });
    }

    void GenerateFile(string fileName, string name, float bpm, string level, float time)
    {
        FileInfo fi = new FileInfo(Application.dataPath + @"\" + fileName + ".txt");
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
        writer.WriteLine("Header");
        writer.WriteLine("Name:" + name);
        writer.WriteLine("Bpm:" + bpm);
        writer.WriteLine("Level:" + level);
        writer.WriteLine("Time" + time);
        writer.WriteLine("EndHeader");
        for (int i = 0; i < bpm / 60 / 4 * time; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                writer.WriteLine("#" + i.ToString("D3") + j.ToString("D2") + " 0 0 0 0 0");
            }
        }
        writer.Close();
    }

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
        streamReader.Close();
    }

    void ModifyText(string path, int startPos, int pos, int[] values)
    {

        ReadTextFile(out string[] lines , path);

        StreamWriter streamWriter = new StreamWriter(path, false); 
        if (streamWriter == StreamWriter.Null)
            return;
        string tempLine = "#" + (pos / 16).ToString("D3") + (pos % 16).ToString("D2");
        for (int i = 0; i < values.Length; i++)
        {
            tempLine += " " + values[i].ToString();
        }
        lines[startPos + pos] = tempLine;
        foreach(string line in lines)
        {
            streamWriter.Write(line + "\n");
        }
        streamWriter.Close();
    }
    void ReadHeader(string[] lines)
    {
        bool isReadingHeader = false;
        foreach (string line in lines)
        {
            string tempLine = line.Substring(0, line.Length - 1);
            if (line.Equals("HEADER"))
            {
                Debug.Log("readingHeader");
                isReadingHeader = true;
            }
            if (line.Equals("ENDHEADER"))
            {
                Debug.Log("endOfHeader");
                isReadingHeader = false;
                return;
            }
            if (isReadingHeader)
            {
                string[] tempString = tempLine.Split(':');
                if (dic.TryGetValue(tempString[0], out var infoType))
                {
                    SaveInfo(infoType, tempString[1]);
                }
            }
        }
    }
   
    void SaveInfo(InfoType infoType, string value)
    {
        switch(infoType)
        {
            case InfoType.Name:
                // 저장해주는 구문
                break;
            case InfoType.Bpm:
                break;
            case InfoType.Level:
                break;
        }
    }
}
