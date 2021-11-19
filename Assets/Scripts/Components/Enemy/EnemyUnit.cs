using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyUnit : MonoBehaviour, IHealth
{
    [SerializeField] GameObject _enemyModel;
    [SerializeField] protected float _maxHealth = 5f;
    [SerializeField] protected float _damagePerHit = 1f;

    EnemyGridPlacement _enemyPlacement;
    int _tileCoverageCount = 0, _currentRevealedCount = 0;
    List<GridTileProcessor> _tilesCovered = new List<GridTileProcessor>();

    protected bool _isRevealed;

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
            gridTiles.OnClicked -= CheckIfFullyRevealed;
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
            tile.OnClicked += CheckIfFullyRevealed;
            tile.OnClicked += DecreaseHealth;
        }
    }

    private void CheckIfFullyRevealed()
    {
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


    public abstract void DecreaseHealth();

    public abstract void Death();
}
