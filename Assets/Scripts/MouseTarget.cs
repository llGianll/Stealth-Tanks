using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    public Action<Collider> OnChangeTarget = delegate { };
    RaycastHit _hit, _previousHit;
    public static MouseTarget Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if(Physics.Raycast(ray, out _hit))
        {
            if (HasTargetChanged())
            {
                OnChangeTarget(_hit.collider);
            }
        }

    }

    private bool HasTargetChanged()
    {
        bool changed = (_previousHit.collider != _hit.collider) ? true : false;

        if (changed)
            _previousHit = _hit;

        return changed;
            
    }
}
