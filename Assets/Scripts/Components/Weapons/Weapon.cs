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
        //GameObject targeting = Instantiate(_targetMode.gameObject, transform.position, Quaternion.identity);
        //targeting.transform.parent = this.transform;
    }

    void Update()
    {
        
    }
}
