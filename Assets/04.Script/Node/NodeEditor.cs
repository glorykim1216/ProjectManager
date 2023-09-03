using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Serialization<T>
{
    public Serialization(List<T> _target) => target = _target;
    public List<T> target;
}

public class NodeEditor : MonoBehaviour
{
    private string _nodeDataFielPath = $"{Application.dataPath}/Resources/Node";
    
    public Transform[] _trNode;
    public GameObject _goNodeSample;

    public void Save(string nodeName)
    {

        for (int i = 0; i < _trNode.Length; i++)
        {
            SaveNode(_trNode[i]);
        }

        void SaveNode(Transform trNode)
        {
            List<NodeData> nodeDataList = new List<NodeData>();
            var nodeList = trNode.GetComponentsInChildren<Node>();
            for (int i = 0; i < nodeList.Length; i++)
            {
                nodeDataList.Add(nodeList[i].GetNodeData());
            }

            string jsonData = JsonUtility.ToJson(new Serialization<NodeData>(nodeDataList));
            JsonParser.Instance.SaveToJsonFile($"{_nodeDataFielPath}/{nodeName}/{nodeName}_{trNode.name}.json", jsonData);
        }

        AssetDatabase.Refresh();
    }

    public bool Load(string nodeName)
    {
        for (int i = 0; i < _trNode.Length; i++)
        {
            LoadNode(_trNode[i]);
        }

        void LoadNode(Transform trNode)
        {
            List<NodeData> nodeDataList = JsonParser.Instance.LoadJsonFile<Serialization<NodeData>>($"{_nodeDataFielPath}/{nodeName}/{nodeName}_{trNode.name}.json").target;
            for (int i = 0; i < nodeDataList.Count; i++)
            {
                var go = Instantiate(_goNodeSample, trNode);
                go.name = $"Node_{i}";
                var node = go.GetComponent<Node>();
                node.transform.position = nodeDataList[i]._pos;
                node._type = nodeDataList[i]._type;
            }
        }

        return true;
    }
}
