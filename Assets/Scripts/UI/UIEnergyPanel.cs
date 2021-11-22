using System;
using TMPro;
using UnityEngine;

public class UIEnergyPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _energyText;

    private void Start()
    {
        EnergyManager.Instance.OnCurrentEnergyChange += UpdateEnergyText;
    }

    private void OnDisable()
    {
        EnergyManager.Instance.OnCurrentEnergyChange += UpdateEnergyText;
    }

    private void UpdateEnergyText(int currentEnergy, int maxEnergy)
    {
        _energyText.text = currentEnergy + "/" + maxEnergy;
    }

}
