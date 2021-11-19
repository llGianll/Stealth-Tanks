using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    [SerializeField] GameObject _enemyUnitPrefab;
    [SerializeField] [Range(0, 20)] int _spawnAmount = 2;

    public GameObject EnemyUnitPrefab => _enemyUnitPrefab;
    public int SpawnAmount => _spawnAmount;
}

[CreateAssetMenu(menuName = "Level/Enemy Spawn List")]
public class EnemySpawnListSO : ScriptableObject
{
    //[Todo] use hashset to only get unique entries, or check _enemyUnitPrefab duplicates 
    [SerializeField]List<EnemySpawnData> _enemySpawnList = new List<EnemySpawnData>();

    public List<EnemySpawnData> EnemySpawnListData => _enemySpawnList;


}
