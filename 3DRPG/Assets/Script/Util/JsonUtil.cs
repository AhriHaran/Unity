using UnityEngine;
using UnityEditor;
using System.IO;

public class JsonUtil
{
    public static string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public static T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    public static void CreateJson(string FileName, string jsonData)
    {
        FileStream fileStream = new FileStream(FileRoute(FileName), FileMode.Create);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public static bool FileCheck(string FileName)
    {
        //파일 경로에 해당 JSON 파일이 존재하는가?
        if (File.Exists(FileRoute(FileName)))
        {
            return true;
        }
        else
            return false;
    }

    public static string FileRoute(string FileName)
    {
        string Path = Application.dataPath + "/Resources/Json"; //json 기본 경로
        return string.Format("{0}/{1}.json", Path, FileName);
    }

    public static T LoadJson<T>(string FileName)
    {
        FileStream fileStream = new FileStream(FileRoute(FileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];

        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string Json = System.Text.Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(Json);
    }
}