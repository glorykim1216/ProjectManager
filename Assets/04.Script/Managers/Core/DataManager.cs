using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using Newtonsoft.Json;
using UnityEditor;

public interface ILoader<key, Value>
{
    Dictionary<key, Value> MakeDict(); 
}

public class DataManager
{
    public Dictionary<int, Data.CarBasicData> CarBasicDic { get; private set; } = new Dictionary<int, Data.CarBasicData>();

    public void Init()
    {
        CarBasicDic = LoadJson<Data.CarBasicDataLoader, int, Data.CarBasicData>("CarBasicData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"{path}");
        return JsonConvert.DeserializeObject<Loader>(textAsset.text);
    }
}

