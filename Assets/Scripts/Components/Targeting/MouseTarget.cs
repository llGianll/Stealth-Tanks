using System;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    //[SerializeField] GameObject _targetModeGO;
    [SerializeField] LayerMask _gridTileLM;
    ITargeting _targetMode;

    RaycastHit _hit, _previousHit;
    Collider _hitCollider;

    public ITargeting TargetMode
    {
        get { return _targetMode; }
        set { _targetMode = value; }
    }

    public Collider HitCollider => _hitCollider;

    public Action OnChangeTarget = delegate { };

    public Action OnClicked = delegate { };

    public static MouseTarget Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        //_targetMode = _targetModeGO.GetComponent<ITargeting>();
    }

    // Update is called once per frame
    void Update()
    {
        TargetWithRaycast();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("CLICK MOUSE ");
            OnClicked();
        }
    }

    private void TargetWithRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, _gridTileLM))
        {
            _hitCollider = _hit.collider;

            if (HasTargetChanged())
            {
                OnChangeTarget();
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
