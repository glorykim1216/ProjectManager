using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    private string _nodeDataFielPath = $"{Application.dataPath}/Resources/Node";

    public List<NodeData> GetNodeData(string nodeName, T_NodeParent type)
    {
        List<NodeData> nodeDataList = JsonParser.Instance.LoadJsonFile<Serialization<NodeData>>($"{_nodeDataFielPath}/{nodeName}/{nodeName}_{type}.json").target;

        return nodeDataList;
    }

    //public List<NodeData> GetCenterNodeData(string nodeName)
    //{
    //    List<NodeData> nodeDataList = JsonParser.Instance.LoadJsonFile<Serialization<NodeData>>($"{_nodeDataFielPath}/{nodeName}/{nodeName}_Center.json").target;

    //    return nodeDataList;
    //}

    //public List<NodeData> GetBestNodeData(string nodeName)
    //{
    //    List<NodeData> nodeDataList = JsonParser.Instance.LoadJsonFile<Serialization<NodeData>>($"{_nodeDataFielPath}/{nodeName}/{nodeName}_Best.json").target;
        
    //    return nodeDataList;
    //}
}
