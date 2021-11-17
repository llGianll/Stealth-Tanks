using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject _gridTile;

    //[Header("Grid Size")]
    //[SerializeField] [Range(1, 20)] int XCount = 5;
    //[SerializeField] [Range(1, 20)] int ZCount = 5;
    [SerializeField] GridSizeSO _gridSize;

    List<GridTileProcessor> _gridTileProcessors = new List<GridTileProcessor>();

    public Action<List<GridTileProcessor>> OnFinishedGridGeneration = delegate { };

    public static GridGenerator Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector3 groundScale = _gridTile.transform.localScale;
        Vector3 firstCellPos = transform.position;

        for (int i = 0; i < _gridSize.XCount; i++)
        {
            for (int j = 0; j < _gridSize.ZCount; j++)
            {
                Vector3 cellPosition = new Vector3(firstCellPos.x + i, firstCellPos.y, firstCellPos.z + j);
                GameObject gridTile = Instantiate(_gridTile, cellPosition, Quaternion.identity);
                gridTile.transform.parent = this.transform;
                gridTile.GetComponent<GridTileProcessor>().CellIndex = _gridTileProcessors.Count;
                _gridTileProcessors.Add(gridTile.GetComponent<GridTileProcessor>());
            }
        }

        //EnemySpawner.Instance.SpawnEnemies(_gridTileProcessors); //[Fix]: weird two-way dependency, switch to be dependent to this class 
        OnFinishedGridGeneration(_gridTileProcessors);
    }

    private Vector3 CalculateFirstCellPos()
    {
        //calculates the first position of the first cell based on the dimensions of the grid and centralize the grid to this position's center 
        Vector3 midpoint = new Vector3(_gridSize.XCount * _gridTile.transform.localScale.x / 2, transform.position.y, _gridSize.ZCount * _gridTile.transform.localScale.z / 2);
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
