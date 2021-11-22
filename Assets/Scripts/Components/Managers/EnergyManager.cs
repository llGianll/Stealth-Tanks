using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] MaxEnergySO _maxEnergySO;

    int _currentEnergy;
    public static EnergyManager Instance;

    public Action<int, int> OnCurrentEnergyChange = delegate { };
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ReplenishEnergyToFull();
    }

    public bool DecreaseEnergy(int energyCost)
    {
        if(energyCost > _currentEnergy)
        {
            //play sfx for not enough energy 
            return false;
        }


        _currentEnergy -= energyCost;
        _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _maxEnergySO.EnergyPerTurn);

        OnCurrentEnergyChange(_currentEnergy, _maxEnergySO.EnergyPerTurn);

        return true;
    }

    public void ReplenishEnergyToFull()
    {
        _currentEnergy = _maxEnergySO.EnergyPerTurn;
        OnCurrentEnergyChange(_currentEnergy, _maxEnergySO.EnergyPerTurn);
    }
}
