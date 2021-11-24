using System;
using UnityEngine;

public class UnitHealth : MonoBehaviour, IHealth
{
    [SerializeField] protected float _maxHealth = 4f;
    [SerializeField] protected float _damagePerHit = 1f;

    private bool _isDead;

    public float CurrentHealth { get; set; }

    public Action<float, float> OnHealthUpdate = delegate { };

    EnemyGridPlacement _enemyPlacement;

    public void DecreaseHealth()
    {

    }
    public void Death()
    {
        
    }


}
