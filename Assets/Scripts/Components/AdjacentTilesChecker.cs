﻿using System;
using System.Collections.Generic;
using UnityEngine;
// transform.forward - right Side, transform.right - down side
public class AdjacentTilesChecker : MonoBehaviour
{
    GridTileProcessor _up;
    GridTileProcessor _down;
    GridTileProcessor _left;
    GridTileProcessor _right;
    GridTileProcessor _currentTile;


    public GridTileProcessor Up => _up;
    public GridTileProcessor Down => _down;
    public GridTileProcessor Left => _left;
    public GridTileProcessor Right => _right;

    [SerializeField] float _rayLength = 0.6f;

    private RaycastHit _hit;

    private void Awake()
    {
        _currentTile = GetComponent<GridTileProcessor>();
    }

    private void OnEnable()
    {
        GridGenerator.Instance.OnFinishedGridGeneration += SetAdjacentReferences;
    }

    private void OnDisable()
    {
        GridGenerator.Instance.OnFinishedGridGeneration -= SetAdjacentReferences;
    }


    private void SetAdjacentReferences(List<GridTileProcessor> _gridTiles)
    {
        //up 
        if(Physics.Raycast(transform.position, -transform.right * _rayLength, out _hit))
            _up = _hit.collider.GetComponent<GridTileProcessor>();

        //down
        if(Physics.Raycast(transform.position, transform.right * _rayLength, out _hit))
            _down = _hit.collider.GetComponent<GridTileProcessor>();

        //left
        if(Physics.Raycast(transform.position, -transform.forward * _rayLength, out _hit))
            _left = _hit.collider.GetComponent<GridTileProcessor>();

        //right
        if(Physics.Raycast(transform.position, transform.forward * _rayLength, out _hit))
            _right = _hit.collider.GetComponent<GridTileProcessor>();
    }

    public void HorizontalChecker()
    {
        MouseTarget.Instance.TargetMode.AddTarget(_currentTile);
        _currentTile.IsTargeted = true;
        CheckLeft(this);
        CheckRight(this);
    }

    public void VerticalChecker()
    {
        MouseTarget.Instance.TargetMode.AddTarget(_currentTile);
        _currentTile.IsTargeted = true;
        CheckUp(this);
        CheckDown(this);
    }

    //[Revisit Notes] Check direction functions recursively search adjacent tiles through raycasts. Seems to be only useful for Line Targeting so why is the logic even here? 
    public AdjacentTilesChecker CheckLeft(AdjacentTilesChecker adjacentChecker)
    {
        //[Refactor] duplication of logic 
        if (adjacentChecker.Left == null)
        {
            return null;
        }

        MouseTarget.Instance.TargetMode.AddTarget(adjacentChecker.Left);

        adjacentChecker.Left.IsTargeted = true;

        return CheckLeft(adjacentChecker.Left.AdjacentChecker);
    }

    public AdjacentTilesChecker CheckRight(AdjacentTilesChecker adjacentChecker)
    {
        if (adjacentChecker.Right == null)
        {
            return null;
        }

        MouseTarget.Instance.TargetMode.AddTarget(adjacentChecker.Right);

        adjacentChecker.Right.IsTargeted = true;

        return CheckRight(adjacentChecker.Right.AdjacentChecker);
    }

    public AdjacentTilesChecker CheckUp(AdjacentTilesChecker adjacentChecker)
    {
        if (adjacentChecker.Up == null)
            return null;

        MouseTarget.Instance.TargetMode.AddTarget(adjacentChecker.Up);

        adjacentChecker.Up.IsTargeted = true;

        return CheckUp(adjacentChecker.Up.AdjacentChecker);
    }

    public AdjacentTilesChecker CheckDown(AdjacentTilesChecker adjacentChecker)
    {
        if (adjacentChecker.Down == null)
            return null;

        MouseTarget.Instance.TargetMode.AddTarget(adjacentChecker.Down);

        adjacentChecker.Down.IsTargeted = true;

        return CheckDown(adjacentChecker.Down.AdjacentChecker);
    }
}