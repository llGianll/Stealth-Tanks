using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWheelInput : MonoBehaviour, IWeaponCycleInputReader
{
    public int WeaponIndex { get; private set; }
    public Action<IWeaponCycleInputReader> OnInputPressed { get; set; }

    int _weaponsCount;
    WeaponManager _weaponManager;

    private void Awake()
    {
        OnInputPressed = delegate { };
        _weaponManager =  GetComponent<WeaponManager>();
        _weaponsCount = transform.childCount;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0 || Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            CalculateAndSetWeaponIndex();
            OnInputPressed(this);
        }
    }

    private void CalculateAndSetWeaponIndex()
    {
        WeaponIndex = _weaponManager.ActiveWeaponIndex - (int)Mathf.Sign(Input.GetAxisRaw("Mouse ScrollWheel")); //decrement when scrolling up, increment when down 
        if (WeaponIndex < 0)
            WeaponIndex = _weaponsCount - 1;
        else if (WeaponIndex >= _weaponsCount)
            WeaponIndex = 0;
    }
}
