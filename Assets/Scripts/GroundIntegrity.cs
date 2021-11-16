using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundIntegrity : MonoBehaviour, IHealth
{
    [SerializeField] float _maxHealth = 100;
    [SerializeField] float _damagePerHit = 50;
    
    float _currentHealth;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        rb.useGravity = true;
        rb.isKinematic = false;
    }
}
