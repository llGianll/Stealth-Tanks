using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    [SerializeField] GameObject _enemyUnitPrefab;
    [SerializeField] [Range(0, 20)] int _spawnCount = 2;
    [SerializeField] Sprite _enemyIcon;
    [SerializeField] string _id;

    //public GameObject EnemyUnitPrefab => _enemyUnitPrefab;
    //public int SpawnAmount => _spawnAmount;
    //public Sprite EnemyIcon => _enemyIcon;

    public GameObject EnemyUnitPrefab
    {
        get { return _enemyUnitPrefab; }
        set { _enemyUnitPrefab = value; }
    }

    public int Count
    { 
        get { return _spawnCount; } 
        set { _spawnCount = value; } 
    }

    public Sprite EnemyIcon
    {
        get { return _enemyIcon; }
        set { _enemyIcon = value; }
    }

    public string ID
    {
        get { return _id; }
        set { _id = value; }
    }

}
