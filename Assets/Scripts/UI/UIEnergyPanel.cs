using System;
using TMPro;
using UnityEngine;

public class UIEnergyPanel : MonoBehaviour
{
    [Header("Scriptable Object References")]
    [SerializeField] EnergyManager _energyManager;
    [Header("UI References")]
    [SerializeField] TextMeshProUGUI _energyText;

    private void OnEnable()
    {
       _energyManager.OnCurrentEnergyChange += UpdateEnergyText;

    }

    private void OnDisable()
    {
        _energyManager.OnCurrentEnergyChange -= UpdateEnergyText;
    }

    private void UpdateEnergyText(int currentEnergy, int maxEnergy)
    {
        _energyText.text = currentEnergy + "/" + maxEnergy;
    }

}
