using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    [SerializeField] TurnCounterSO _turnCounter;

    public Action<int> OnCurrentTurnChange = delegate { };
    public Action<int> OnLose = delegate { };

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _turnCounter.TurnCount = 1;
        OnCurrentTurnChange(_turnCounter.TurnCount);
    }

    public void EndTurn()
    {
        EnergyManager.Instance.ReplenishEnergyToFull();
        _turnCounter.TurnCount++;
        OnCurrentTurnChange(_turnCounter.TurnCount);
        if(_turnCounter.TurnCount > _turnCounter.TurnsToClear)
        {
            GameManager.Instance.FailedLevel();
        }
    }
}
