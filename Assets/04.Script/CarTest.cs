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

    void Start()
    {
        _tr = this.transform;

        _targetList = _nodeManager.GetBestNodeData("Node_01");

        target = _targetList[_targetIndex]._pos;
    }

    void Update()
    {
        float targetSpeed = 10;
        if (_targetList[_targetIndex]._type == T_Node.Straight)
            targetSpeed = 38.0f;
        else if (_targetList[_targetIndex]._type == T_Node.CurveIn)
            targetSpeed = 10.0f;
        else if (_targetList[_targetIndex]._type == T_Node.Curve)
            targetSpeed = 8.0f;
        else if (_targetList[_targetIndex]._type == T_Node.CurveOut)
            targetSpeed = 15.0f;
        else if (_targetList[_targetIndex]._type == T_Node.DRS)
            targetSpeed = 45.0f;

        _speed = Mathf.Lerp(_speed, targetSpeed, Time.deltaTime);
        _sqrMagnitudeValue = _speed;

        var pos = target - _tr.position;
        var sqr = Vector3.SqrMagnitude(pos);
        //Debug.Log(sqr);
        if (sqr <= _sqrMagnitudeValue)
        {
            if (_targetIndex < _targetList.Count-1)
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
