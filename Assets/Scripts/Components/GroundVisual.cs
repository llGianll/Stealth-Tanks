using System;
using UnityEngine;

public class GroundVisual : MonoBehaviour
{
    const float RGB_MAX = 255f;

    [SerializeField] float _minRGBValue = 100f;

    float _maxRGBValue, _currentRGBValue;

    GridTileProcessor _gridTileProcessor;
    GroundIntegrity _groundIntegrity;
    MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _gridTileProcessor = transform.parent.GetComponent<GridTileProcessor>();
        _groundIntegrity = GetComponent<GroundIntegrity>();
        //_col = _gridTileProcessor.GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _groundIntegrity.OnHealthModified += DarkenGroundMaterial;
    }

    private void OnDisable()
    {
        _groundIntegrity.OnHealthModified -= DarkenGroundMaterial;
    }

    private void DarkenGroundMaterial(float healthPercentage)
    {
        _maxRGBValue = _meshRenderer.material.color.r;
        _currentRGBValue = Mathf.Lerp(_minRGBValue/RGB_MAX, _maxRGBValue, healthPercentage);

        Color newMaterialColor = new Color(_currentRGBValue, _currentRGBValue, _currentRGBValue, _meshRenderer.material.color.a);
        _meshRenderer.material.color = newMaterialColor;
    }

}
