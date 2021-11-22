using UnityEngine;

[CreateAssetMenu(menuName = "Level/Max Energy")]
public class MaxEnergySO : ScriptableObject
{
    //could also contain max energy per level

    [SerializeField] [Range(1, 100)] int _maxEnergyPerTurn = 20;

    public int EnergyPerTurn { get { return _maxEnergyPerTurn; } }
}

