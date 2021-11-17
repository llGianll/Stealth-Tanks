using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderEnemy : MonoBehaviour, IHealth
{
    [SerializeField] float _maxHealth = 2f;
    [SerializeField] float _damagePerHit = 1f;

    float _currentHealth;
    
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
        gameObject.SetActive(false);
    }

}
