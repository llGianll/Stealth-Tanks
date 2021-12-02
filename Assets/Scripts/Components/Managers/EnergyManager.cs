using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/EnergyManager", fileName = "EnergyManager")]
public class EnergyManager : ScriptableObject
{
    [SerializeField] MaxEnergySO _maxEnergySO;
    int _currentEnergy;
    
    //[TODO] Create a custom editor that can display properties so that I can modify values in the inspector and execute an event whenever the value is changed.
    public int CurrentEnergy 
    {
        get { return _currentEnergy; }
        set 
        { 
            _currentEnergy = value;
            OnCurrentEnergyChange(_currentEnergy, _maxEnergySO.EnergyPerTurn);
        } 
    }

    public Action<int, int> OnCurrentEnergyChange = delegate { };

    private void OnEnable()
    {
        //ReplenishEnergyToFull();
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
