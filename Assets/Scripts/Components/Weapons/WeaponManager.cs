using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    List<Weapon> _weapons = new List<Weapon>();
    Weapon _activeWeapon;

    private void Start()
    {
        InitializeWeaponry();

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
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(_weapons[0]);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(_weapons[1]);

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
