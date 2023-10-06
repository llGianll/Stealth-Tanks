using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Targeting : MonoBehaviour
{
    //Whether there's a single tile target or multiple(ex: lines), it'll be handled as 'Target' 
    //single target value = first element, multiple target = iterate through list contents
    public List<GridTileProcessor> Target = new List<GridTileProcessor>();

    private void OnEnable()
    {
        if (MouseTarget.Instance != null)
        {
            MouseTarget.Instance.OnChangeTarget += AcquireTarget;
            AcquireTarget();
        }
    }
    private void OnDisable()
    {
        if (MouseTarget.Instance != null)
        {
            RefreshTargeting();
            MouseTarget.Instance.OnChangeTarget -= AcquireTarget;
        }
    }
    protected virtual void Start() => MouseTarget.Instance.OnChangeTarget += AcquireTarget;

    protected abstract void AcquireTarget();
    protected abstract void RefreshTargeting();
    public abstract void AddTarget(GridTileProcessor target);
    
}
