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
    protected ITargeting _targetMode; 

    public int EnergyCost => _energyCost;
    public Sprite Icon => _weaponIcon;

    private void Awake() => _targetMode = GetComponentInChildren<ITargeting>();

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

    protected virtual void UseWeapon()
    {

    }
}
