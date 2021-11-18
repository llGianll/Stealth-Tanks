using System;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    public Action<Collider> OnChangeTarget = delegate { };
    RaycastHit _hit, _previousHit;
    public static MouseTarget Instance;
    ITargeting _targetMode;
    [SerializeField] GameObject _targetModeGO;

    public ITargeting TargetMode => _targetMode;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _targetMode = _targetModeGO.GetComponent<ITargeting>();

        if (_targetMode != null)
            Debug.Log("ITargeting exists");
    }

    // Update is called once per frame
    void Update()
    {
        TargetWithRaycast();

    }

    private void TargetWithRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out _hit))
        {
            if (HasTargetChanged())
            {
                OnChangeTarget(_hit.collider);
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
}
