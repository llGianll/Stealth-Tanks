using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _totalEnergy = 100;
    [SerializeField] EnemySpawnListSO _enemySpawnList;

    int _currentEnergy;
    List<EnemySpawnData> _enemyLiveCount = new List<EnemySpawnData>();

    public static GameManager Instance;

    public Action<List<EnemySpawnData>, string> OnEnemyCountUpdate = delegate{ };
    public Action OnGameEnd = delegate { };

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        _currentEnergy = _totalEnergy;
        CopyEnemySpawnList();
    }

    private void CopyEnemySpawnList()
    {
        int index = 0;

        foreach (var item in _enemySpawnList.EnemySpawnListData)
        {
            _enemyLiveCount.Add(new EnemySpawnData());
            _enemyLiveCount[index].EnemyUnitPrefab = item.EnemyUnitPrefab;
            _enemyLiveCount[index].Count = item.Count;
            _enemyLiveCount[index].EnemyIcon = item.EnemyIcon;
            _enemyLiveCount[index].ID = item.ID;

            index++;
        }
    }

    public void DecreaseEnemyCount(string id)
    {
        foreach (var item in _enemyLiveCount)
        {
            if (item.ID == id)
                item.Count--;

        }

        OnEnemyCountUpdate(_enemyLiveCount, id);
    }

    public void DecreaseEnergy(int energyCost)
    {
        _currentEnergy -= energyCost;
        _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _totalEnergy);
    }
}
