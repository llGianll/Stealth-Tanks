using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyUnit : MonoBehaviour, IHealth
{
    [SerializeField] protected GameObject _enemyModel;
    [SerializeField] protected float _maxHealth = 5f;
    [SerializeField] protected float _damagePerHit = 1f;

    public string ID { get; set; }

    EnemyGridPlacement _enemyPlacement;
    List<GridTileProcessor> _tilesCovered = new List<GridTileProcessor>();

    protected bool _isRevealed, _isDead;

    public virtual float CurrentHealth { get; set; }

    private void Awake()
    {
        _enemyPlacement = GetComponent<EnemyGridPlacement>();
    }

    private void OnEnable()
    {
        _enemyPlacement.OnFinishedPlacement += RegisterGridTileOnClick;
    }

    private void OnDisable()
    {
        _enemyPlacement.OnFinishedPlacement += RegisterGridTileOnClick;

        foreach (var gridTiles in _tilesCovered)
        {
            gridTiles.OnClicked -= ProcessClick;
            gridTiles.OnClicked -= DecreaseHealth;
        }
    }

    private void Start()
    {
        _enemyModel.SetActive(false);
        CurrentHealth = _maxHealth;
    }

    private void RegisterGridTileOnClick(List<GridTileProcessor> gridTiles)
    {
        _tilesCovered = gridTiles;
        foreach (var tile in gridTiles)
        {
            tile.OnClicked += ProcessClick;
            //tile.OnClicked += DecreaseHealth;
        }
    }

    private void ProcessClick()
    {
        DecreaseHealth();
        CheckIfFullyRevealed();
    }

    private void CheckIfFullyRevealed()
    {
        if (_isRevealed)
            return;

        foreach (var gridTiles in _tilesCovered)
        {
            if (!gridTiles.IsClicked)
                return;
        }

        //foreach (var gridTiles in _tilesCovered)
        //{
        //    gridTiles.OnClicked -= CheckIfFullyRevealed;
        //}

        _isRevealed = true;
        _enemyModel.SetActive(true);
    }

    public void DecreaseHealth()
    {
        if (!_isRevealed)
            return;

        CurrentHealth -= _damagePerHit;

        if (CurrentHealth <= 0 && !_isDead)
            Death();
    }

    public virtual void Death()
    {
        GameManager.Instance.DecreaseEnemyCount(ID);
        _isDead = true;
    }
}
