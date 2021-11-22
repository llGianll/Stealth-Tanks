using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTileTargeting : Targeting, ITargeting
{
    GridTileProcessor _targetTile;

    private void OnEnable()
    {
        if (MouseTarget.Instance != null)
        {
            MouseTarget.Instance.OnChangeTarget += Targeting;
            MouseTarget.Instance.OnClicked += ClickTarget;
            Targeting();
            Debug.Log("Enabled by:" + gameObject.name);
        }

    }

    private void OnDisable()
    {
        if(MouseTarget.Instance != null)
        {
            RefreshTargeting();
            MouseTarget.Instance.OnChangeTarget -= Targeting;
            MouseTarget.Instance.OnClicked -= ClickTarget;
        }
    }

    private void Start()
    {
        MouseTarget.Instance.OnChangeTarget += Targeting;
        MouseTarget.Instance.OnClicked += ClickTarget;
    }

    private void ClickTarget()
    {
        if (_targetTile != null)
            _targetTile.Clicked();
    }

    private void Update()
    {

    }

    private void Targeting()
    {
        AddTarget(MouseTarget.Instance.HitCollider.GetComponent<GridTileProcessor>());
    }

    public void AddTarget(GridTileProcessor target)
    {
        RefreshTargeting();

        if (target == null)
            return;

        _targetTile = target;
        _targetTile.IsTargeted = true;
    }

    public void RefreshTargeting()
    {
        if (_targetTile == null)
            return;

        _targetTile.IsTargeted = false;
        _targetTile = null;
    }
}
