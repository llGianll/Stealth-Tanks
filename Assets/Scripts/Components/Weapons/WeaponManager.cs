using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    List<Weapon> _weapons = new List<Weapon>();
    //Weapon _activeWeapon;
    int _activeWeaponIndex;

    private void Start()
    {
        InitializeWeaponry();
        _activeWeaponIndex = 0;

        if (_weapons.Count <= 0)
            return;

        SwitchWeapon(_weapons[0]);
    }

    private void InitializeWeaponry()
    {
        foreach (Transform weapon in transform)
        {
            _weapons.Add(weapon.GetComponent<Weapon>());
        }
    }

    private void Update()
    {
        //[Refactor]temporary hotkey inputs 
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    SwitchWeapon(_weapons[0]);
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //    SwitchWeapon(_weapons[1]);
        MouseWheelWeaponCycle();
    }

    private void MouseWheelWeaponCycle()
    {
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0 || Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            CalculateWeaponIndex();
            SwitchWeapon(_weapons[_activeWeaponIndex]);
        }



    }

    private void CalculateWeaponIndex()
    {
        _activeWeaponIndex -= (int)Mathf.Sign(Input.GetAxisRaw("Mouse ScrollWheel")); //decrement when scrolling up, increment when down 
        if (_activeWeaponIndex < 0)
            _activeWeaponIndex = _weapons.Count - 1;
        else if (_activeWeaponIndex >= _weapons.Count)
            _activeWeaponIndex = 0;
    }

    private void SwitchWeapon(Weapon weapon)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

            if (child.GetComponent<Weapon>() == weapon)
            {
                MouseTarget.Instance.TargetMode = weapon.gameObject.transform.GetComponentInChildren<ITargeting>();
                child.gameObject.SetActive(true);
            }
        }

        
    }
}
