using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected int _energyCost = 2;
    [SerializeField] Sprite _weaponIcon;
    protected ITargeting _targetMode;

    public int EnergyCost => _energyCost;
    public Sprite Icon => _weaponIcon;

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
            if (EnergyManager.Instance.DecreaseEnergy(_energyCost))
            {
                CameraShake.Instance.Shake();
                _targetMode.ClickTarget();
            }
        }
    }
}
