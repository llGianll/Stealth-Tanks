using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyUnit : MonoBehaviour, IHealth
{
    //[Refactor] move health related variables and functions to its own class 
    [SerializeField] protected GameObject _enemyModel;
    [SerializeField] protected float _maxHealth = 5f;
    [SerializeField] protected float _damagePerHit = 1f;

    public string ID { get; set; }

    EnemyGridPlacement _enemyPlacement;
    List<GridTileProcessor> _tilesCovered = new List<GridTileProcessor>();

    protected bool _isRevealed, _isDead;

    public virtual float CurrentHealth { get; set; }

    public Action<float, float> OnHealthUpdate = delegate { };


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
        if (_isDead)
            return;

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

        _isRevealed = true;
        _enemyModel.SetActive(true);
        OnHealthUpdate(CurrentHealth, _maxHealth);
    }

    public void DecreaseHealth()
    {
        if (!_isRevealed)
            return;

        CurrentHealth -= _damagePerHit;
        OnHealthUpdate(CurrentHealth, _maxHealth);

        if (CurrentHealth <= 0 && !_isDead)
            Death();
    }

    public virtual void Death()
    {
        GameManager.Instance.DecreaseEnemyCount(ID);
        _isDead = true;
    }
}
