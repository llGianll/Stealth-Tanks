using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] int _spawnCount = 5;
    [SerializeField] float _spawnYOffset = 1f;

    //public static EnemySpawner Instance;

    private void Awake()
    {
        //if (Instance == null)
        //    Instance = this;
        //else
        //    Destroy(gameObject);
    }

    private void OnDisable()
    {
        GridGenerator.Instance.OnFinishedGridGeneration -= SpawnEnemies;
    }

    private void Start()
    {
        GridGenerator.Instance.OnFinishedGridGeneration += SpawnEnemies;
    }


    public void SpawnEnemies(List<GridTileProcessor> gridTileProcessors)
    {
        //[Todo] Copy the list, then remove the content with a given index once it's used 

        int cellCount = gridTileProcessors.Count;

        if (_spawnCount > gridTileProcessors.Count)
            return;

        for (int i = 0; i < _spawnCount;)
        {
            int rand = UnityEngine.Random.Range(0, cellCount);

            if (gridTileProcessors[rand].IsOccupied)
                continue;

            Spawn(gridTileProcessors[rand]);
            gridTileProcessors[rand].IsOccupied = true;
            i++;
        }
    }

    private void Spawn(GridTileProcessor gridTile)
    {
        Vector3 tilePosition = gridTile.transform.position;
        Vector3 enemyPos = new Vector3(tilePosition.x, tilePosition.y + _spawnYOffset, tilePosition.z);
        GameObject enemy = Instantiate(_enemy, enemyPos, Quaternion.identity);
        enemy.GetComponent<Enemy>().GridTile = gridTile;
        enemy.transform.parent = gridTile.transform;
        
        //circumvent the onenable/ondisable issue
        enemy.SetActive(false);
        enemy.SetActive(true);
    }
}
