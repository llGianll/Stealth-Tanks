using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundIntegrity : MonoBehaviour, IHealth, IDeath
{
    [Header("Ground Crack")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioEventSO _sfx_Crack;

    float _maxHealth;
    float _damagePerHit = 1;
    
    Rigidbody _rb;
    GridTileProcessor _gridTileProcessor;

    public Action<float, float> OnHealthUpdate { get; set; }

    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public float CurrentHealth { get; set; }
    public bool IsDead { get; set; }
    public Action<string> OnDeath { get; set; }
    public string ID { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _gridTileProcessor = transform.parent.GetComponent<GridTileProcessor>();
        OnHealthUpdate = delegate { }; //initialize event
        OnDeath = delegate { };
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
        _maxHealth = GridGenerator.Instance.MaxGroundIntegrityData.Integrity;
        RandomizeStartingHealth();
        OnHealthUpdate(CurrentHealth, _maxHealth);
    }

    private void RandomizeStartingHealth()
    {
        int min = 1;
        int max = (int)(_maxHealth + _damagePerHit);
        CurrentHealth = UnityEngine.Random.Range(min, max);
    }

    public void DecreaseHealth()
    {
        CurrentHealth -= _damagePerHit;

        OnHealthUpdate(CurrentHealth, _maxHealth);

        if (CurrentHealth <= 0)
            DropGroundTile();
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    private void DropGroundTile()
    {
        _sfx_Crack.Play(_audioSource);
        _rb.useGravity = true;
        _rb.isKinematic = false;
    }
}
