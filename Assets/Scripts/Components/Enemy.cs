using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] float _maxHealth = 2f;
    [SerializeField] float _damagePerHit = 1f;

    float _currentHealth;
    public GridTileProcessor GridTile { get; set; }

    private void OnEnable()
    {
        if(GridTile != null)
            GridTile.OnClicked += DecreaseHealth;
    }

    private void OnDisable()
    {
        if (GridTile != null)
            GridTile.OnClicked -= DecreaseHealth;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void DecreaseHealth()
    {
        Debug.Log("Damaged Tank");
        _currentHealth -= _damagePerHit;

        if (_currentHealth <= 0)
            Death();
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

}
