using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _projectileSpeed = 2f;
    Rigidbody _rb;
    protected bool _isMoving = false;
    GridTileProcessor _target;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rb.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _rb.velocity = transform.forward * _projectileSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GridTileProcessor>())
        {
            CameraShake.Instance.Shake();
            _target.Clicked();
            gameObject.SetActive(false);
        }
    }

    public void Move(GridTileProcessor target)
    {
        _isMoving = true;
        _target = target;
    }
}
