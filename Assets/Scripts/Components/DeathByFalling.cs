using System;
using System.Collections.Generic;
using UnityEngine;

public class DeathByFalling : MonoBehaviour, IDeath
{
    [Header("Object References")]
    [SerializeField] TileDetector _tileDetector;
    [SerializeField] UnitGridPlacement _unitGridPlacement;
    [SerializeField] GameObject _unit;
    [Header("Raycast Check")]
    [SerializeField] List<Transform> _raycastChecks = new List<Transform>();
    [SerializeField] float _raycastLength = 1f;
    [SerializeField] LayerMask _groundLM;

    IDeath death;
    bool _isPlacedOnGrid = false;

    public bool IsDead { get; set; }
    public Action<string> OnDeath { get; set; }
    public string ID { get; set; }

    private void Awake()
    {
        OnDeath = delegate { }; //initialize event 
        death = _unit.GetComponent<IDeath>();
    }

    private void OnEnable()
    {
        _unitGridPlacement.OnFinishedPlacement += EnableGroundChecks;
            
    }

    private void EnableGroundChecks(List<GridTileProcessor> obj)
    {
        _isPlacedOnGrid = true;
    }

    private void Update()
    {
        if (_isPlacedOnGrid)
        {
            if(!IsDead && !IsGroundUnderneath())
            {
                EnableFall();
            }
        }
    }

    private bool IsGroundUnderneath()
    {
        RaycastHit hit;
        foreach (var downCast in _raycastChecks)
        {
            if (Physics.Raycast(downCast.position, Vector3.down, out hit, _raycastLength, _groundLM))
            {
                return true;
            }
        }

        return false;
    }

    private void EnableFall()
    {
        //if (IsAlreadyDead())
        //    return;

        _unit.GetComponent<Rigidbody>().isKinematic = false;
        _unit.GetComponent<Rigidbody>().useGravity = true;
    }

    private bool IsAlreadyDead()
    {
        if (death.IsDead) //if unit died due to health == 0, don't process death here
            return true;
        else
            return false;
    }

    public void Death()
    {
        if (IsAlreadyDead())
            return;

        IsDead = true;

        if(death != null)
        {
            //death var is a reference to the unit's ID(which is only initialized there and to IsDead var 
            ID = death.ID;
            death.IsDead = true; //to avoid scenarios where unit can be killed more than once
        }
        ID = (death != null) ? death.ID : "";
        GameManager.Instance.DecreaseEnemyCount(ID);
        _unit.SetActive(false);
        //OnDeath("");
    }
}
