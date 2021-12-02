using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/TurnManager", fileName = "TurnManager")]
public class TurnManager : ScriptableObject
{
    [Header("Scriptable Object References")]
    [SerializeField] EnergyManager _energyManager;
    
    [Header("Class Fields")]
    [SerializeField] TurnCounterSO _turnCounter;

    public Action<int> OnCurrentTurnChange = delegate { };

    private void OnEnable()
    {
        _turnCounter.TurnCount = 1;
        OnCurrentTurnChange(_turnCounter.TurnCount);
        _energyManager.ReplenishEnergyToFull();
    }

    public void Initialize()
    {
        _turnCounter.TurnCount = 1;
        OnCurrentTurnChange(_turnCounter.TurnCount);
        _energyManager.ReplenishEnergyToFull();
    }

    public void EndTurn()
    {
        _energyManager.ReplenishEnergyToFull();
        _turnCounter.TurnCount++;
        OnCurrentTurnChange(_turnCounter.TurnCount);

        if(_turnCounter.TurnCount > _turnCounter.TurnsToClear)
        {
            if(GameManager.Instance != null)
                GameManager.Instance.FailedLevel();
        }
    }
}
