using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] GameObject _content;
    [SerializeField] Animator _animator;

    private void OnEnable()
    {
        _animator.SetTrigger("PopOut");
    }
}
