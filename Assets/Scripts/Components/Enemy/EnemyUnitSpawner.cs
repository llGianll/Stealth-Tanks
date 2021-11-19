using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitSpawner : MonoBehaviour
{
    [SerializeField] EnemySpawnListSO _spawnList;
    [SerializeField] float _spawnYOffset = 0.6f;
    private void OnDisable()
    {
        GridGenerator.Instance.OnFinishedGridGeneration -= SpawnEnemies;
    }

    private void Start()
    {
        GridGenerator.Instance.OnFinishedGridGeneration += SpawnEnemies;
    }

    private void SpawnEnemies(List<GridTileProcessor> gridTiles)
    {
        if (gridTiles.Count <= 0)
            return;

        //[Todo] [POST] determine if there are no spaces available on the grid based on each enemy cell coverage 

        foreach (var enemy in _spawnList.EnemySpawnListData)
        {
            for (int i = 0; i < enemy.SpawnAmount;)
            {
                int rand = UnityEngine.Random.Range(0, gridTiles.Count);

                if (gridTiles[rand].IsOccupied)
                    continue;



                if (!IsSpawned(gridTiles[rand], enemy.EnemyUnitPrefab))
                    continue;

                i++;
            }
        }
    }
    private bool IsSpawned(GridTileProcessor gridTile, GameObject enemyPrefab)
    {
        //[Todo] set enemy spawner as parent of enemy prefabs for organization
        Vector3 tilePosition = gridTile.transform.position;
        Vector3 enemyPos = new Vector3(tilePosition.x, tilePosition.y + _spawnYOffset, tilePosition.z);

        GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);

        EnemyGridPlacement enemyPlacement = enemy.GetComponent<EnemyGridPlacement>();
        return enemyPlacement.IsPlacementDetermined();
    }
}