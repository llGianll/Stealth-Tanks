using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBomber : Weapon
{
    Vector3 _startPoint, _endpoint;
    bool _isHorizontal;

    [SerializeField] float _spawnAndEndOffset = 5f;
    private void GetStartAndEndPoint()
    {
        Vector3 firstTile = _targetMode.TargetTiles[0].transform.position;
        Vector3 nextTile = _targetMode.TargetTiles[1].transform.position;

        CheckIfHorizontal(firstTile, nextTile);

        //get unit vector of the position of the first two tiles on the list 
        Vector3 tileDirecton = (firstTile - nextTile).normalized;
        Vector3 differenceToOrigin = OriginDifference(firstTile);
        tileDirecton = FlipDirection(tileDirecton);

        _startPoint = StartPointWithOffset(differenceToOrigin);
        _endpoint = _startPoint + (tileDirecton * (9+_spawnAndEndOffset*2)) ; //[refactor] use grid size data later to remove magic number 9
    }

    #region Calculating Start and End Point Helper Functions
    private Vector3 StartPointWithOffset(Vector3 differenceToOrigin)
    {
        Vector3 startPoint;

        if (_isHorizontal)
            startPoint = (Vector3.zero + differenceToOrigin) + new Vector3(0, 0, -_spawnAndEndOffset);
        else
            startPoint = (Vector3.zero + differenceToOrigin) + new Vector3(-_spawnAndEndOffset, 0, 0);

        return startPoint;
    }

    private Vector3 FlipDirection(Vector3 tileDirection)
    {
        if (_isHorizontal)
        {
            if (Vector3.Dot(Vector3.forward, tileDirection) < 0)
                return -tileDirection;
            else
                return tileDirection;
        }
        else
        {
            if (Vector3.Dot(Vector3.down, tileDirection) < 0)
                return -tileDirection;
            else
                return tileDirection;
        }
    }

    private Vector3 OriginDifference(Vector3 firstTile)
    {
        if (_isHorizontal)
            return new Vector3(firstTile.x, 0, 0);
        else
            return new Vector3(0, 0, firstTile.z);
    }

    private void CheckIfHorizontal(Vector3 firstTile, Vector3 nextTile)
    {
        if (firstTile.z != nextTile.z) //if the z of the two tiles not equal, the orientation is horizontal
            //Debug.Log("Horizontal");
            _isHorizontal = true;
        else
            _isHorizontal = false;

    } 
    #endregion

    private void Update()
    {
        GetStartAndEndPoint();
    }
}
