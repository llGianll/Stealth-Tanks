using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    [SerializeField] protected int _energyCost = 2;
    [SerializeField] protected GameObject _targetMode;

    void Start()
    {

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
            GameManager.Instance.DecreaseEnergy(_energyCost);
        }
    }
}
