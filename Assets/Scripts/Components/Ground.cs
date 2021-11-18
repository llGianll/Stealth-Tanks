using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] Material _originalMaterial;
    [SerializeField] Material _targetedMaterial;

    GridTileProcessor _gridTileProcessor;
    MeshRenderer _meshRenderer;
    Collider _col;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _gridTileProcessor = transform.parent.GetComponent<GridTileProcessor>();
        _col = _gridTileProcessor.GetComponent<Collider>();
    }

    private void OnEnable()
    {
        //_gridTileProcessor.OnMouseHover += SwapGroundMaterial;
    }

    private void OnDisable()
    {
        //_gridTileProcessor.OnMouseHover -= SwapGroundMaterial;
    }

    public void SwapGroundMaterial()
    {
        if (_gridTileProcessor.IsSelected)
            _meshRenderer.material = _targetedMaterial; 
        else 
            _meshRenderer.material = _originalMaterial; 
    }

}
