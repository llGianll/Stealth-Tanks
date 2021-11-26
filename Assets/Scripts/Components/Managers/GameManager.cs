using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemySpawnListSO _enemySpawnList;

    int _currentEnergy;
    int _currentEnemyCount;

    List<EnemySpawnData> _enemyLiveCount = new List<EnemySpawnData>();

    public static GameManager Instance;

    public Action<List<EnemySpawnData>, string> OnEnemyCountUpdate = delegate{ };
    public Action<bool> OnGameEnd = delegate { };

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        OnEnemyCountUpdate += CheckGameEnd;
    }

    private void OnDisable()
    {
        OnEnemyCountUpdate -= CheckGameEnd;
    }
    private void CheckGameEnd(List<EnemySpawnData> arg1, string arg2)
    {
        //Show End Game UI with button, pause the game 
        if(_currentEnemyCount <= 0)
        {
            //Time.timeScale = 0f;
            OnGameEnd(true);
        }
    }

    void Start()
    {
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

            _currentEnemyCount+= item.Count;
        }
    }

    public void DecreaseEnemyCount(string id)
    {
        foreach (var item in _enemyLiveCount)
        {
            if (item.ID == id)
            {
                item.Count--;
                _currentEnemyCount--;
            }

        }

        OnEnemyCountUpdate(_enemyLiveCount, id);
    }

    public void FailedLevel()
    {
        //Time.timeScale = 0f;
        OnGameEnd(false);
    }

    [ContextMenu("Test Win")]
    public void TestWin()
    {
        OnGameEnd(true);
    }

}
