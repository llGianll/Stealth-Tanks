using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StealthBomber : MonoBehaviour
{
    [SerializeField] LayerMask _gridTileLM;

    RaycastHit _hit, _previousHit;
    Rigidbody _rb;
    string _bombID;
    bool _isInitialized = false;
    int _currentTargetIndex = 0;

    List<Vector3> _targetPositions = new List<Vector3>();
    public Action OnChangedTarget = delegate { };

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _hit = new RaycastHit();
        _previousHit = new RaycastHit();
    }

    private void OnDisable()
    {
        _isInitialized = false;
    }

    private void Update()
    {
        if(_isInitialized)
            DownCast();
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rb.velocity = velocity;
    }

    private void DownCast()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, Mathf.Infinity, _gridTileLM))
        {
            if (HasTargetChanged())
            {
                Vector3 targetTilePosition = _targetPositions[_currentTargetIndex];
                GameObject bomb = PooledObjectManager.Instance.GetPooledObject(_bombID);
                bomb.transform.position = new Vector3(targetTilePosition.x, 
                                                      transform.position.y - 0.5f, 
                                                      targetTilePosition.z);
                bomb.SetActive(true);

                _currentTargetIndex++;
            }
        }

    }

    private bool HasTargetChanged()
    {
        bool changed = (_previousHit.collider != _hit.collider) ? true : false;

        if (changed)
            _previousHit = _hit;

        return changed;

    }

    public void InitializeTargets(List<GridTileProcessor> targets, bool isHorizontal, string bombID, SpawnScreenSide spawnSide)
    {
        _targetPositions.Clear();
        _currentTargetIndex = 0;

        foreach (var target in targets)
        {
            _targetPositions.Add(target.transform.position);
        }

        _bombID = bombID;

        if (isHorizontal)
        {
            if(spawnSide == SpawnScreenSide.Bottom)
                _targetPositions = _targetPositions.OrderBy(x => x.z).ToList();
            else 
                _targetPositions = _targetPositions.OrderByDescending(x => x.z).ToList(); 


        }
        else
        {
            if(spawnSide == SpawnScreenSide.Top)
                _targetPositions = _targetPositions.OrderBy(x => x.x).ToList();
            else 
                _targetPositions = _targetPositions.OrderByDescending(x => x.x).ToList();


        }

        _isInitialized = true;

    }
}
