using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/Enemy Spawn List")]
public class EnemySpawnListSO : ScriptableObject
{
    //[Todo] use hashset to only get unique entries, or check _enemyUnitPrefab duplicates 
    [SerializeField]List<EnemySpawnData> _enemySpawnList = new List<EnemySpawnData>();

    public List<EnemySpawnData> EnemySpawnListData => _enemySpawnList;


}
