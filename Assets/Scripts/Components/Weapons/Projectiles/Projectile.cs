using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _projectileSpeed = 2f;
    Rigidbody _rb;
    GridTileProcessor _target;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GridTileProcessor>())
        {
            CameraShake.Instance.Shake();
            other.GetComponent<GridTileProcessor>().Clicked();
            gameObject.SetActive(false);
        }
    }

    public void Move(GridTileProcessor target)
    {
        //_target = target;
        _rb.velocity = transform.forward * _projectileSpeed;
    }

    public void MoveWithGravity(GridTileProcessor target)
    {
        //_target = target;
    }
}
