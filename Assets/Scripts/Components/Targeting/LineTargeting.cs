using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTargeting : Targeting, ITargeting
{
    enum TargetingOrientation {Horizontal, Vertical};
    TargetingOrientation _targetingOrientation;

    List<GridTileProcessor> _targets = new List<GridTileProcessor>();
    
    private void Start()
    {
        _targetingOrientation = TargetingOrientation.Horizontal;
        MouseTarget.Instance.OnChangeTarget += Targeting;
    }

    private void OnEnable()
    {
        if(MouseTarget.Instance != null)
        {
            MouseTarget.Instance.OnChangeTarget += Targeting;
            Targeting();
        }

    }

    private void OnDisable()
    {
        if (MouseTarget.Instance != null)
        {
            RefreshTargeting();
            MouseTarget.Instance.OnChangeTarget -= Targeting;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //[Refactor] Create an Input class to centralize all inputs 
        {
            foreach (var target in _targets)
            {
                target.Clicked();
            }

        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchTargetingOrientation();
        }
    }

    private void SwitchTargetingOrientation()
    {
        _targetingOrientation = (_targetingOrientation == TargetingOrientation.Horizontal) ?
                                                TargetingOrientation.Vertical :
                                                TargetingOrientation.Horizontal;

        Targeting();
    }

    private void Targeting()
    {
        RefreshTargeting();

        AdjacentTilesChecker adjacentChecker = MouseTarget.Instance.HitCollider.GetComponent<AdjacentTilesChecker>();

        if (adjacentChecker == null)
            return;

        switch (_targetingOrientation)
        {
            case TargetingOrientation.Horizontal:
                adjacentChecker.HorizontalChecker();
                break;
            case TargetingOrientation.Vertical:
                adjacentChecker.VerticalChecker();
                break;
            default:
                break;
        }
    }

    public void AddTarget(GridTileProcessor target)
    {
        _targets.Add(target);
    }
    public void RefreshTargeting()
    {
        foreach (var target in _targets)
        {
            target.IsTargeted = false;
        }

        _targets.Clear();
    }
}

