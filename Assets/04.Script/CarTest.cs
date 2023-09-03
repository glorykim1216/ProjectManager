using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTest : MonoBehaviour
{
    private Transform _tr;
    public Vector3 target;
    public float _speed;
    public float _handlingSpeed = 5.0f;

    public float _sqrMagnitudeValue = 0.5f;
    public List<NodeData> _targetList;
    private int _targetIndex;
    public NodeManager _nodeManager;
    public float _break = 1;

    void Start()
    {
        _tr = this.transform;

        _targetList = _nodeManager.GetNodeData("Node_01", T_NodeParent.Best);

        target = _targetList[_targetIndex]._pos;
    }

    void Update()
    {
        float nodeTypeSpeed = 15;
        if (_targetList[_targetIndex]._type == T_Node.Straight)
            nodeTypeSpeed = 40.0f;
        else if (_targetList[_targetIndex]._type == T_Node.CurveIn)
            nodeTypeSpeed = 12.0f;
        else if (_targetList[_targetIndex]._type == T_Node.Curve)
            nodeTypeSpeed = 10.0f;
        else if (_targetList[_targetIndex]._type == T_Node.CurveOut)
            nodeTypeSpeed = 14.0f;
        else if (_targetList[_targetIndex]._type == T_Node.DRS)
            nodeTypeSpeed = 50.0f;

        float targetSpeed = 15;
        // 현재 속도와 앞차 or 노드속도 비교
        float breakValue = _speed / nodeTypeSpeed;
        if (breakValue > 1)
        {
            targetSpeed = nodeTypeSpeed - breakValue;
        }
        else
        {
            breakValue = 1;
            targetSpeed = nodeTypeSpeed;
        }

        _speed = Mathf.Lerp(_speed, targetSpeed, Time.deltaTime * breakValue);
        _sqrMagnitudeValue = targetSpeed;

        var pos = target - _tr.position;
        var sqr = Vector3.SqrMagnitude(pos);
        //Debug.Log(sqr);
        if (sqr <= _sqrMagnitudeValue)
        {
            if (_targetIndex < _targetList.Count - 1)
            {
                _targetIndex++;
            }
            else
            {
                _targetIndex = 0;
            }
            target = _targetList[_targetIndex]._pos;
            pos = target - _tr.position;
        }
        _tr.rotation = Quaternion.Lerp(_tr.rotation, Quaternion.LookRotation(pos), _handlingSpeed * Time.deltaTime);

        _tr.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
