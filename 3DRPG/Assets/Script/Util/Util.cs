using System.IO;
using UnityEngine;

public partial class Util
{
    public static char[] PATH_SEPERATOR = new char[] { '/', '\\' };

    public static void UpdateResolutionToCamera(Camera cam, float nPerWidth, float nPerHeight)
    {
        float resolutionX = Screen.width / nPerWidth;
        float resolutionY = Screen.height / nPerHeight;

        if (resolutionX < resolutionY)
        {
            if (null == cam.targetTexture)
            {
                float origin = cam.orthographicSize;
                cam.orthographicSize = (resolutionY / resolutionX) * origin;
            }
        }
    }

    //특정 Tag name object를 picking 하는 함수
    public static GameObject RayCastTagObject(string tagName, float rayLength)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.tag == tagName)
                return hit.collider.gameObject;
        }

        return null;
    }

    //특정 Layer이름을 가지는 Object를 picking하는 함수
    public static bool RayCastLayerObject(string LayerName, float rayLength, ref GameObject pickedObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength, LayerMask.NameToLayer(LayerName)))
        {
            pickedObject = hit.collider.gameObject;
            return true;
        }

        return false;
    }

    public static string GetPriceStringFromValue(int value)
    {
        return string.Format("{0:#,###0}", value);
    }

    public static void DeleteFilesByExtension(string dirPath, string extension)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
        FileInfo[] fileInfos;

        string targetExt = string.Format("*.{0}", extension);
        fileInfos = dirInfo.GetFiles(targetExt, SearchOption.AllDirectories);
        if (fileInfos.Length <= 0)
            return;

        for (int i = 0; i < fileInfos.Length; ++i)
        {
            //만약 ReadOnly 속성이 있는 파일이 있다면 지울때 에러가 나므로 속성을 Normal로 바꿔 놓는다.
            if (fileInfos[i].Attributes == FileAttributes.ReadOnly)
                fileInfos[i].Attributes = FileAttributes.Normal;

            fileInfos[i].Delete();
        }
    }

    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    T JsonToObject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    public void CreateJson(string createPath, string FileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, FileName), FileMode.Create);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public bool FileCheck(string FilePath)
    {
        if(System.IO.File.Exists(FilePath))
        {
            return true;
        }
        else
            return false;
    }

    public T LoadJson<T>(string loadPath, string FileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, FileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];

        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string Json = System.Text.Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(Json);
    }
}