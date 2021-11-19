using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _totalEnergy = 100;
    int _currentEnergy;

    public static GameManager Instance;

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
    }

 
    void Update()
    {
        
    }

    public void DecreaseEnergy(int energyCost)
    {
        _currentEnergy -= energyCost;
        _currentEnergy = Mathf.Clamp(_currentEnergy, 0, _totalEnergy);
    }
}
