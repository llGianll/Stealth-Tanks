using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileVisual : MonoBehaviour
{
    [SerializeField] float _inactiveMaterialAlpha = 0f;
    [SerializeField] float _activeMaterialAlpha = .15f;

    GridTileProcessor _gridTileProcessor;
    MeshRenderer _meshRenderer;
    Color _originalMaterialColor, _activeMaterialColor, _inactiveMaterialColor;


    private void Awake()
    {
        _gridTileProcessor = GetComponentInParent<GridTileProcessor>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        TileColorSetup();
    }

    private void OnEnable()
    {
        _gridTileProcessor.OnSelectionChange += HighlightTile;
    }

    private void OnDisable()
    {
        _gridTileProcessor.OnSelectionChange -= HighlightTile;
    }

    private void TileColorSetup()
    {
        _originalMaterialColor = _meshRenderer.material.color;
        _activeMaterialColor = new Color(_originalMaterialColor.r, _originalMaterialColor.g, _originalMaterialColor.b, _activeMaterialAlpha);
        _inactiveMaterialColor = new Color(_originalMaterialColor.r, _originalMaterialColor.g, _originalMaterialColor.b, _inactiveMaterialAlpha);
    }
    
    private void HighlightTile()
    {
        _meshRenderer.material.color = (_gridTileProcessor.IsTargeted) ? _activeMaterialColor : _inactiveMaterialColor;
    }
}
