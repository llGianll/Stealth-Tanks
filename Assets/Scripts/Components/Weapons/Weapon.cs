using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected int _energyCost = 2;
    protected ITargeting _targetMode;

    private void Awake()
    {
        _targetMode = GetComponentInChildren<ITargeting>();
    }

    private void OnEnable()
    {
        if(MouseTarget.Instance != null)
            MouseTarget.Instance.OnClicked += UseWeapon;
    }

    private void OnDisable()
    {
        if (MouseTarget.Instance != null)
            MouseTarget.Instance.OnClicked -= UseWeapon;
    }

    private void UseWeapon()
    {
        if(MouseTarget.Instance.HitCollider.GetComponent<GridTileProcessor>() != null)
        {
            if(EnergyManager.Instance.DecreaseEnergy(_energyCost))
                _targetMode.ClickTarget();
        }
    }
}
