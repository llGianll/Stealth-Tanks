using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyUnit : MonoBehaviour, IHealth, IDeath
{
    //[Refactor] move health related variables and functions to its own class 
    //[Refactor] also need to make a general class for revealing other units, instead of just enemies 
    [SerializeField] protected GameObject _enemyModel;
    [SerializeField] protected float _maxHealth = 5f;
    [SerializeField] protected float _damagePerHit = 1f;

    public string ID { get; set; }

    UnitGridPlacement _enemyPlacement;
    List<GridTileProcessor> _tilesCovered = new List<GridTileProcessor>();

    protected bool _isRevealed, _isDead;

    public virtual float CurrentHealth { get; set; }
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }
    public Action<float, float> OnHealthUpdate { get; set; }
    public Action<string> OnDeath { get; set; }
    public Action OnReveal = delegate { };

    private void Awake()
    {
        _enemyPlacement = GetComponent<UnitGridPlacement>();
        OnHealthUpdate = delegate { };
        OnDeath = delegate { };
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

        //check if any tile the enemy covers is not yet marked 
        foreach (var gridTiles in _tilesCovered)
        {
            if (!gridTiles.IsClicked)
                return;
        }

        RevealUnit();

        OnHealthUpdate(CurrentHealth, _maxHealth); //call to initialize health bar values upon enemy being revealed
    }

    private void RevealUnit()
    {
        _isRevealed = true;
        OnReveal();
        _enemyModel.SetActive(true);
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
