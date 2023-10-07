using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int _energyCost = 2;
    [SerializeField] Sprite _weaponIcon;
    [SerializeField] protected string _projectileID; //[Refactor] get objects from object pool without relying on ID 
    
    [Header("Scriptable Object References")]
    [SerializeField] protected EnergyManager _energyManager;
    protected Targeting _targetMode; 

    public int EnergyCost => _energyCost;
    public Sprite Icon => _weaponIcon;

    private void Awake() => _targetMode = GetComponentInChildren<Targeting>();

    protected virtual void OnEnable()
    {
        if(MouseTarget.Instance != null)
            MouseTarget.Instance.OnClicked += UseWeapon;
    }

    protected virtual void OnDisable()
    {
        if (MouseTarget.Instance != null)
            MouseTarget.Instance.OnClicked -= UseWeapon;
    }

    protected virtual bool CanFire()
    {
        if (MouseTarget.Instance.HitCollider.GetComponent<GridTileProcessor>() != null)
            if (_energyManager.DecreaseEnergy(_energyCost))
                return true;

        return false;
    }

    protected abstract void UseWeapon();

}
