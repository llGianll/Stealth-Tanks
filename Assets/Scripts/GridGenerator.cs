using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] Transform _groundPrefab;

    [Header("Grid Size")]
    [SerializeField] [Range(1, 20)] int _xCount = 5;
    [SerializeField] [Range(1, 20)] int _zCount = 5;

    [SerializeField] List<GridContent> _gridContents = new List<GridContent>();
    
    public List<GridContent> GridContents 
    {
        get { return _gridContents; }
    }

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector3 groundScale = _groundPrefab.localScale;
        Vector3 firstCellPos = transform.position;

        for (int i = 0; i < _xCount; i++)
        {
            for (int j = 0; j < _zCount; j++)
            {
                Vector3 cellPosition = new Vector3(firstCellPos.x + i, firstCellPos.y, firstCellPos.z + j);
                GameObject ground = Instantiate(_groundPrefab.gameObject, cellPosition, Quaternion.identity);
                ground.transform.parent = this.transform;
                _gridContents.Add(new GridContent(ground, _gridContents.Count));
            }
        }

        EnemySpawner.instance.SpawnEnemies(_gridContents);
    }

    private Vector3 CalculateFirstCellPos()
    {
        //calculates the first position of the first cell based on the dimensions of the grid and centralize the grid to this position's center 
        Vector3 midpoint = new Vector3(_xCount * _groundPrefab.localScale.x / 2, transform.position.y, _zCount * _groundPrefab.localScale.z / 2);
        float xPos = transform.position.x - midpoint.x;
        float zPos = transform.position.z - midpoint.z;

        Debug.Log(xPos + " , " + zPos);

        return new Vector3(xPos,
                           transform.position.y,
                           zPos);
    }

    private void OnDrawGizmosSelected()
    {
        //Vector3 gridDimension = new Vector3(_xCount * _groundPrefab.localScale.x, 
        //                                    _groundPrefab.localScale.y , 
        //                                    _zCount * _groundPrefab.localScale.z);
        //Gizmos.DrawWireCube(transform.position, gridDimension);
    }
}

[System.Serializable]
public class GridContent
{
    public GameObject _groundTile;
    public int _cellIndex;
    public bool Occupied { get; set; }

    public GridContent(GameObject groundTile, int index)
    {
        _groundTile = groundTile;
        _cellIndex = index;
    }
}
