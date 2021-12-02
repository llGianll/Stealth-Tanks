using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStarHolder : MonoBehaviour
{
    Animator _animator;
    AudioEventSO _sfx_Star;
    [SerializeField] AudioSource _audioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayPopOutAnimation(AudioEventSO audioEvent)
    {
        _sfx_Star = audioEvent;
        _animator.SetTrigger("PopOut");
    }

    public void PlaySFX()
    {
        _sfx_Star.Play(_audioSource);
    }
}
