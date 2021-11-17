using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMarker : MonoBehaviour
{
    [SerializeField] Color _occupiedColor;
    [SerializeField] Color _unoccupiedColor;

    MeshRenderer _meshRenderer;
    GridTileProcessor _gridTileProcessor;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _gridTileProcessor = transform.parent.GetComponent<GridTileProcessor>();
    }

    private void OnEnable()
    {
        if (_gridTileProcessor != null)
            _gridTileProcessor.OnClicked += ShowMarker;
    }
    private void OnDisable()
    {
        if (_gridTileProcessor != null)
            _gridTileProcessor.OnClicked -= ShowMarker;
    }

    private void Start()
    {
        _meshRenderer.enabled = false;    
    }

    private void ShowMarker()
    {
        _meshRenderer.material.color = (_gridTileProcessor.IsOccupied) ? _occupiedColor : _unoccupiedColor;
        _meshRenderer.enabled = true;
    }

    
}
