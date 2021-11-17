using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] Material _originalMaterial;
    [SerializeField] Material _targetedMaterial;
    
    MeshRenderer _meshRenderer;
    Collider _col;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _col = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        MouseTarget.Instance.OnChangeTarget += SwapGroundMaterial;
    }

    private void OnDisable()
    {
        MouseTarget.Instance.OnChangeTarget -= SwapGroundMaterial;
    }

    public void SwapGroundMaterial(Collider target)
    {
        if (_col == target)
            _meshRenderer.material = _targetedMaterial; 
        else 
            _meshRenderer.material = _originalMaterial; 
            
    }

}
