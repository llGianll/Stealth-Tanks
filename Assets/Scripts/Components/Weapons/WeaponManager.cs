using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Audio variables")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioEventSO _sfx_WeaponSwitch;

    List<Weapon> _weapons = new List<Weapon>();
    int _activeWeaponIndex;
    bool _allowWeaponSwitch; //don't allow weapon switch when not focused on the game 
    bool _isInitialized = false; //sound flag for sfx to not play at the very start of the level when weapon is initialized at Start()

    public int ActiveWeaponIndex => _activeWeaponIndex;

    List<IWeaponCycleInputReader> _weaponCycleInputReaders = new List<IWeaponCycleInputReader>();

    public Action<Sprite, int> OnSwitchWeapon = delegate { };

    private void Awake()
    {
        _weaponCycleInputReaders = GetComponents<IWeaponCycleInputReader>().ToList();
        foreach (var inputReader in _weaponCycleInputReaders)
        {
            inputReader.OnInputPressed += ProcessInput;
        }
    }

    private void OnDisable()
    {
        foreach (var inputReader in _weaponCycleInputReaders)
        {
            inputReader.OnInputPressed -= ProcessInput;
        }
    }

    private void ProcessInput(IWeaponCycleInputReader inputReader)
    {
        _activeWeaponIndex = inputReader.WeaponIndex;
        SwitchWeapon(_weapons[inputReader.WeaponIndex]);
    }

    private void Start()
    {
        InitializeWeaponry();
        _activeWeaponIndex = 0;

        if (_weapons.Count <= 0)
            return;

        InitializeActiveWeapon();
    }

    private void InitializeActiveWeapon()
    {
        SwitchWeapon(_weapons[0]);
        _isInitialized = true;
    }

    private void InitializeWeaponry()
    {
        foreach (Transform weapon in transform)
        {
            _weapons.Add(weapon.GetComponent<Weapon>());
        }
    }

    private void SwitchWeapon(Weapon weapon)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in transform)
        {
            if (child.GetComponent<Weapon>() == weapon)
            {
                MouseTarget.Instance.TargetMode = weapon.gameObject.transform.GetComponentInChildren<ITargeting>();
                child.gameObject.SetActive(true);
            }
        }

        if (_isInitialized)
            _sfx_WeaponSwitch.Play(_audioSource);

        OnSwitchWeapon(weapon.Icon, weapon.EnergyCost);
    }

    private void OnApplicationFocus(bool focus)
    {
        //dont allow weapon switch when game window isn't focused, very useful when working in the editor
        _allowWeaponSwitch = focus;
    }
}

