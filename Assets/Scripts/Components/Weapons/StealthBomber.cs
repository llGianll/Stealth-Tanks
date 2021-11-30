using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthBomber : MonoBehaviour
{
    bool _isActivated = false;
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        _isActivated = true;
        _rb.velocity = velocity;
    }
}
