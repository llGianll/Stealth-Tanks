using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] int _spawnCount = 5;
    [SerializeField] float _spawnYOffset = 1f;

    public static EnemySpawner instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SpawnEnemies(List<GridContent> gridContents)
    {
        int cellCount = gridContents.Count;

        if (_spawnCount > gridContents.Count)
            return;

        for (int i = 0; i < _spawnCount;)
        {
            int rand = UnityEngine.Random.Range(0, cellCount);

            if (gridContents[rand].Occupied)
                continue;

            Spawn(gridContents[rand]._groundTile.transform.position);
            gridContents[rand].Occupied = true;
            i++;
        }
    }

    private void Spawn(Vector3 tilePosition)
    {
        Vector3 enemyPos = new Vector3(tilePosition.x, tilePosition.y + _spawnYOffset, tilePosition.z);
        GameObject enemy = Instantiate(_enemy, enemyPos, Quaternion.identity);
        enemy.transform.parent = this.transform;
    }
}
