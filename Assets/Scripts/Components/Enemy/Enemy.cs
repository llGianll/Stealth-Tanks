using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] float _maxHealth = 2f;
    [SerializeField] float _damagePerHit = 1f;
    [SerializeField] GameObject _enemyModel;

    float _currentHealth;
    public GridTileProcessor GridTile { get; set; }
    public float CurrentHealth { get; set; }

    bool _isRevealed;

    private void OnEnable()
    {
        if(GridTile != null)
        {
            GridTile.OnClicked += DecreaseHealth;
            GridTile.OnClicked += Reveal;
        }
    }

    private void OnDisable()
    {
        if (GridTile != null)
        {
            GridTile.OnClicked -= DecreaseHealth;
            GridTile.OnClicked -= Reveal;
        }
    }

    private void Start()
    {
        _enemyModel.SetActive(false);
        _currentHealth = _maxHealth;
    }

    private void Reveal()
    {
        if (!_isRevealed)
        {
            _enemyModel.SetActive(true);
            _isRevealed = true;
        }
    }

    public void DecreaseHealth()
    {
        if (!_isRevealed)
            return;

        _currentHealth -= _damagePerHit;

        if (_currentHealth <= 0)
            Death();
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.5f);
    }
}
