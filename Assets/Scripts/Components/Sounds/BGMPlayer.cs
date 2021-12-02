using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioEventSO _bgm;
    private void OnEnable()
    {
        if(_bgm != null)
            _bgm.Play(_audioSource);
    }

    public void SetBGM(AudioEventSO bgm)
    {
        _bgm = bgm;
        _bgm.Play(_audioSource);
    }
}
