using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundIntegrity : MonoBehaviour, IHealth
{
    [SerializeField] float _maxHealth = 100;
    [SerializeField] float _damagePerHit = 50;
    
    float _currentHealth;
    Rigidbody _rb;
    GridTileProcessor _gridTileProcessor;

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
        _currentHealth = _maxHealth;    
    }

    public void DecreaseHealth()
    {
        _currentHealth -= _damagePerHit;
        
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
