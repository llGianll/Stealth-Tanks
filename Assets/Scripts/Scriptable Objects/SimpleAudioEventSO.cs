using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Simple_Audio", menuName = "AudioEvent/Simple")]
public class SimpleAudioEventSO : AudioEventSO
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] [Range(0,5)] float _volume;
    [SerializeField] [Range(0, 5)] float _pitch;
    public override void Play(AudioSource audioSource)
    {
        if (_audioClip == null) return;

        InitializeAudioSource(audioSource);
        audioSource.Play();
    }

    public override void PlayOneShot(AudioSource audioSource)
    {
        if (_audioClip == null) return;

        InitializeAudioSource(audioSource);
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void InitializeAudioSource(AudioSource audioSource)
    {
        audioSource.clip = _audioClip;
        audioSource.volume = _volume;
        audioSource.pitch = _pitch;
    }
}
