using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionRadius : MonoBehaviour
{
    BoxCollider col;
    List<Collider> _hitColliders = new List<Collider>();
    [SerializeField] LayerMask _tileLayerMask;
    [SerializeField] GameObject _explosionPrefab;

    private void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        GridTileProcessor gridTiles = other.gameObject.GetComponent<GridTileProcessor>();
        if (gridTiles != null)
        {
            gridTiles.Clicked();
        }
    }

    public void Explode()
    {
        _hitColliders = (Physics.OverlapBox(transform.position, transform.localScale / 2, transform.rotation, _tileLayerMask)).ToList();
        foreach (var item in _hitColliders)
        {
            item.GetComponent<GridTileProcessor>().Clicked();
        }
    }
}
