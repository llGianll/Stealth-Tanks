using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioEventSO _sfx;
    private void OnEnable()
    {
        _sfx.Play(_audioSource);
    }
}
