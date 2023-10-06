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
        {
            SwitchTargetingOrientation();
        }
    }

    private void SwitchTargetingOrientation()
    {
        _targetingOrientation = (_targetingOrientation == TargetingOrientation.Horizontal) ?
                                                TargetingOrientation.Vertical :
                                                TargetingOrientation.Horizontal;

        AcquireTarget();
    }

    protected override void AcquireTarget()
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
    public override void AddTarget(GridTileProcessor target)
    {
        Target.Add(target);
    }
}

