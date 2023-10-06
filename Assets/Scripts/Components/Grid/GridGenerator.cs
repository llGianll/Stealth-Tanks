using System;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject _gridTile;

    [SerializeField] GridSizeSO _gridSize;
    [SerializeField] MaxGroundIntegritySO _maxGroundIntegrity;

    List<GridTileProcessor> _gridTileProcessors = new List<GridTileProcessor>();

    public Action<List<GridTileProcessor>> OnFinishedGridGeneration = delegate { };

    public static GridGenerator Instance;

    public MaxGroundIntegritySO MaxGroundIntegrityData => _maxGroundIntegrity;

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

        /*
         * [Revisit Notes] 
         * Standard grid generation when using 1D collection. Doesn't use object pooling which is bad performance-wise. 
         */
        for (int i = 0; i < _gridSize.XCount; i++)
        {
            for (int j = 0; j < _gridSize.ZCount; j++)
            {
                Vector3 cellPosition = new Vector3(firstCellPos.x + i, firstCellPos.y, firstCellPos.z + j);
                GameObject gridTile = Instantiate(_gridTile, cellPosition, Quaternion.identity);
                gridTile.transform.parent = this.transform;
                _gridTileProcessors.Add(gridTile.GetComponent<GridTileProcessor>());
            }
        }

        //[Revisit Notes] Signals EnemyUnitSpawner to start spawning enemies and for Grid tiles to start checking and referencing adjacent tiles. 
        OnFinishedGridGeneration(_gridTileProcessors);
    }

    //[Revisit Notes] Unused since I chose to just spawn starting from the position of the gameobject itself. The initial purpose seemed to be to have the grid centralized to the gameobject position. 
    //Seems to work just as intended. 
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

}
