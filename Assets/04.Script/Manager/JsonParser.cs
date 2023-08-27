using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class JsonParser : MonoSingleton<JsonParser>
{
    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public T JsonToOject<T>(string jsonData)
    {
        return JsonUtility.FromJson<T>(jsonData);
    }

    public void SaveToJsonFile(string filePath, string jsonData)
    {
        // 출시전 보안 관련 EncryptAES 주석 풀기
        //string dataAES = EncryptAES.Instance.Encrypt256(jsonData);
        //File.WriteAllText(filePath, dataAES);
        File.WriteAllText(filePath, jsonData);
    }

    public void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public T LoadJsonFile<T>(string fileName)
    {
        string jsonData = File.ReadAllText(fileName);

        // 출시전 보안 관련 EncryptAES 주석 풀기
        //jsonData = EncryptAES.Instance.Decrypt256(jsonData); //해독

        return JsonUtility.FromJson<T>(jsonData);
    }

    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        return JsonUtility.FromJson<T>(jsonData);
    }

    public T LoadJsonFileAES<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        jsonData = EncryptAES.Instance.Decrypt256(jsonData); //해독

        return JsonUtility.FromJson<T>(jsonData);
    }
}