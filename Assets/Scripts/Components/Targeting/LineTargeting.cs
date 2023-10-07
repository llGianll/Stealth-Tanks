using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTargeting : Targeting
{
    enum TargetingOrientation {Horizontal, Vertical};
    TargetingOrientation _targetingOrientation;

    protected override void Start()
    {
        base.Start();
        _targetingOrientation = TargetingOrientation.Horizontal;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            SwitchTargetingOrientation();
    }

    private void SwitchTargetingOrientation()
    {
        _targetingOrientation = (_targetingOrientation == TargetingOrientation.Horizontal) ?
                                                TargetingOrientation.Vertical :
                                                TargetingOrientation.Horizontal;
        AcquireTarget();
    }

    protected override void RefreshTargeting()
    {
        if (Target == null)
            return;

        foreach (var target in Target)
        {
            target.IsTargeted = false;
        }

        Target.Clear();
    }

    public override void AddTarget()
    {
        AdjacentTilesChecker targetTile = MouseTarget.Instance.HitCollider.GetComponent<AdjacentTilesChecker>();

        if (targetTile == null)
            return;

        switch (_targetingOrientation)
        {
            case TargetingOrientation.Horizontal:
                AddHorizontalTargets(targetTile);
                break;
            case TargetingOrientation.Vertical:
                AddVerticalTargets(targetTile);
                break;
            default:
                break;
        }
    }

    void AddHorizontalTargets(AdjacentTilesChecker targetTile)
    {
        Target.Add(targetTile.CurrentTile);
        targetTile.CurrentTile.IsTargeted = true;

        CheckLeft(targetTile);
        CheckRight(targetTile);
    }

    void AddVerticalTargets(AdjacentTilesChecker targetTile)
    {
        Target.Add(targetTile.CurrentTile);
        targetTile.CurrentTile.IsTargeted = true;

        CheckUp(targetTile);
        CheckDown(targetTile);
    }

    #region Horizontal and Vertical Helper Functions
    AdjacentTilesChecker CheckLeft(AdjacentTilesChecker currentTile)
    {
        if (currentTile.Left == null)
            return null;

        Target.Add(currentTile.Left);
        currentTile.Left.IsTargeted = true;

        return CheckLeft(currentTile.Left.AdjacentChecker);
    }

    AdjacentTilesChecker CheckRight(AdjacentTilesChecker currentTile)
    {
        if (currentTile.Right == null)
            return null;

        Target.Add(currentTile.Right);
        currentTile.Right.IsTargeted = true;

        return CheckRight(currentTile.Right.AdjacentChecker);
    }

    AdjacentTilesChecker CheckUp(AdjacentTilesChecker currentTile)
    {
        if (currentTile.Up == null)
            return null;

        Target.Add(currentTile.Up);
        currentTile.Up.IsTargeted = true;

        return CheckUp(currentTile.Up.AdjacentChecker);
    }

    AdjacentTilesChecker CheckDown(AdjacentTilesChecker currentTile)
    {
        if (currentTile.Down == null)
            return null;

        Target.Add(currentTile.Down);
        currentTile.Down.IsTargeted = true;

        return CheckDown(currentTile.Down.AdjacentChecker);
    } 
    #endregion
}

