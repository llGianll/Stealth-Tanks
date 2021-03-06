using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTileTargeting : Targeting, ITargeting
{
    GridTileProcessor _targetTile;

    public GridTileProcessor TargetTile => _targetTile;
    public List<GridTileProcessor> TargetTiles { get; } //not needed here 

    private void OnEnable()
    {
        if (MouseTarget.Instance != null)
        {
            MouseTarget.Instance.OnChangeTarget += Targeting;
            //MouseTarget.Instance.OnClicked += ClickTarget;
            Targeting();
        }

    }

    private void OnDisable()
    {
        if(MouseTarget.Instance != null)
        {
            RefreshTargeting();
            MouseTarget.Instance.OnChangeTarget -= Targeting;
            //MouseTarget.Instance.OnClicked -= ClickTarget;
        }
    }

    private void Start()
    {
        MouseTarget.Instance.OnChangeTarget += Targeting;
        //MouseTarget.Instance.OnClicked += ClickTarget;
    }

    public void ClickTarget()
    {
        if (_targetTile != null)
            _targetTile.Clicked();
    }

    private void Targeting()
    {
        if(MouseTarget.Instance.HitCollider != null) 
        {
            AddTarget(MouseTarget.Instance.HitCollider.GetComponent<GridTileProcessor>());
        }
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
