using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTargeting : Targeting, ITargeting
{
    //Change Orientation(horizontal, vertical) by pressing tab,
    //get tile collider through MouseTarget class event, then check all adjacent tiles by using the component AdjacentTiles
    //for each adjacent tile, depending on the orientation(horizontal - check left/right , vertical - check up/down) recursively set IsSelected of GridTileProcessor to true
    //GridTileProcessor needs to evaluate if it's in-line or not? 

    List<GridTileProcessor> _targets = new List<GridTileProcessor>();
    
    private void Start()
    {
        MouseTarget.Instance.OnChangeTarget += TargetHorizontal;
    }

    private void OnDisable()
    {
        MouseTarget.Instance.OnChangeTarget -= TargetHorizontal;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var target in _targets)
            {
                target.Clicked();
            }

        }
    }

    private void TargetHorizontal(Collider col)
    {
        foreach (var target in _targets)
        {
            target.IsTargeted = false;
        }

        _targets.Clear();

        Debug.Log("Changed Target");
        AdjacentTilesChecker adjacentChecker = col.GetComponent<AdjacentTilesChecker>();

        if (adjacentChecker != null)
            adjacentChecker.HorizontalChecker();
    }

    public void AddTarget(GridTileProcessor target)
    {
        _targets.Add(target);
    }

}
