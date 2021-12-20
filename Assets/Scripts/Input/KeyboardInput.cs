using System;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, IWeaponCycleInputReader
{
    public int WeaponIndex { get; private set; }
    public Action<IWeaponCycleInputReader> OnInputPressed { get; set; }

    int _keyboardNumPressed;

    private void Awake()
    {
        OnInputPressed = delegate { };
    }

    private void Update()
    {
        //[Todo] add an input parser later to remove keycode hardcoding 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponIndex = 1 - 1;
            OnInputPressed(this);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponIndex = 2 - 1;
            OnInputPressed(this);
        }
    }
}
