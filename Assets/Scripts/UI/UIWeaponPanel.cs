using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponPanel : MonoBehaviour
{
    [SerializeField] Image _activeWeaponIcon;
    [SerializeField] TextMeshProUGUI _energyText;
    // Start is called before the first frame update
    void Start()
    {
        WeaponManager.Instance.OnSwitchWeapon += SwitchActiveWeaponUI;
    }

    private void OnDisable()
    {
        WeaponManager.Instance.OnSwitchWeapon -= SwitchActiveWeaponUI;    
    }

    private void SwitchActiveWeaponUI(Sprite weaponIcon, int energyCost)
    {
        _activeWeaponIcon.sprite = weaponIcon;
        _energyText.text = energyCost.ToString("00");
    }

}
