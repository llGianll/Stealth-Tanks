using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundIntegrity : MonoBehaviour, IHealth
{
    float _maxHealth, _currentHealth;
    float _damagePerHit = 1;
    
    Rigidbody _rb;
    GridTileProcessor _gridTileProcessor;

    public Action<float> OnHealthModified = delegate { };

    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _gridTileProcessor = transform.parent.GetComponent<GridTileProcessor>();
    }

    private void OnEnable()
    {
        if(_gridTileProcessor != null)
            _gridTileProcessor.OnClicked += DecreaseHealth;
    }

    private void OnDisable()
    {
        if (_gridTileProcessor != null)
            _gridTileProcessor.OnClicked -= DecreaseHealth;
    }

    private void Start()
    {
        _maxHealth = GridGenerator.Instance.MaxGroundIntegrityData.Integrity;
        //_currentHealth = _maxHealth;
        RandomizeStartingHealth();
        OnHealthModified(GetHealthPercentage());
    }

    private void RandomizeStartingHealth()
    {
        int min = 1;
        int max = (int)(_maxHealth + _damagePerHit);
        _currentHealth = UnityEngine.Random.Range(min, max);
    }

    private float GetHealthPercentage()
    {
        return _currentHealth / _maxHealth;
    }

    public void DecreaseHealth()
    {
        _currentHealth -= _damagePerHit;

        OnHealthModified(GetHealthPercentage());

        if (_currentHealth <= 0)
            Death();
    }

    public void Death()
    {
         DropGroundTile();
    }

    private void DropGroundTile()
    {
        _rb.useGravity = true;
        _rb.isKinematic = false;
    }
}
