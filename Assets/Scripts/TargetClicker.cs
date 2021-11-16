using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetClicker : MonoBehaviour
{
    IHealth _target;
    MouseTarget _mouseTarget;
    Collider _targetCollider;

    private void Awake()
    {
        _mouseTarget = GetComponent<MouseTarget>();
    }

    private void OnEnable()
    {
        _mouseTarget.OnChangeTarget += AssignTarget;
    }


    private void OnDisable()
    {
        _mouseTarget.OnChangeTarget -= AssignTarget;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (_targetCollider == null)
                return;

            _target = _targetCollider.GetComponent<IHealth>();
            if (_target != null)
                _target.DecreaseHealth();

        }
    }
    private void AssignTarget(Collider col)
    {
        _targetCollider = col;
    }


}
