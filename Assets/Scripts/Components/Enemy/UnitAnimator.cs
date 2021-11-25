using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] EnemyUnit _enemyUnit;
    [SerializeField] GameObject _healthBarAnchor;
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _enemyUnit.OnDeath += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        _enemyUnit.OnDeath -= PlayDeathAnimation;
    }

    private void PlayDeathAnimation(string obj)
    {
        _animator.SetBool("IsDead", true);
    }

    public void HideHealthBar()
    {
        if(_healthBarAnchor != null)
            _healthBarAnchor.SetActive(false);
    }
}
