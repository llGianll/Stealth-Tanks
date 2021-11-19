using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetector : MonoBehaviour
{
    List<GridTileProcessor> _gridTilesHit = new List<GridTileProcessor>();
    [SerializeField] List<Transform> _raycastChecks = new List<Transform>();
    [SerializeField] float _raycastLength = 1f;
    [SerializeField] int _tileCoverageCount = 1; 
    public List<GridTileProcessor> GridTilesHit => _gridTilesHit;

    private void OnEnable()
    {
        
    }

    private void ClearHitList()
    {
        _gridTilesHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {

        //GridTileProcessor gridTiles = other.gameObject.GetComponent<GridTileProcessor>();

        //if (gridTiles != null)
        //    _gridTilesHit.Add(gridTiles);
    }

    public bool AllTilesAvailable()
    {
        //Debug.Log(_gridTilesHit.Count);
        ClearHitList();
        DetectAllTiles();

        if (_gridTilesHit.Count <= 0)
            return false;

        //Debug.Log(_gridTilesHit.Count < _tileCoverageCount);

        if (_gridTilesHit.Count < _tileCoverageCount)
        {
            Debug.Log("not enough tile coverage");
            return false;
        }

        foreach (var gridTiles in _gridTilesHit)
        {
            if (gridTiles.IsOccupied)
                return false;
        }

        return true;
    }

    private void DetectAllTiles()
    {
        RaycastHit hit;
        foreach (var downCast in _raycastChecks)
        {
            if(Physics.Raycast(downCast.position, Vector3.down * _raycastLength, out hit))
            {
                GridTileProcessor gridTiles = hit.collider.gameObject.GetComponent<GridTileProcessor>();

                if (gridTiles != null)
                    _gridTilesHit.Add(gridTiles);
            }
        }
    }
}
