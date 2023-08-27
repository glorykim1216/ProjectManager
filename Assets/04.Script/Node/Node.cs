using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeData
{
    public Vector3 _pos = new Vector3();
    public T_Node _type;
}

[ExecuteInEditMode]
public class Node : MonoBehaviour
{
    public T_Node _type;
    
    public NodeData GetNodeData()
    {
        NodeData _data = new NodeData();
        _data._pos = transform.position;
        _data._type = _type;
        return _data;
    }
}
