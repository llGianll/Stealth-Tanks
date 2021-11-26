using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStarHolder : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayPopOutAnimation()
    {
        _animator.SetTrigger("PopOut");
    }
}
