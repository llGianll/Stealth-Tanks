using UnityEngine;

public abstract class AudioEventSO : ScriptableObject
{
    public abstract void Play(AudioSource audioSource);
    public abstract void PlayOneShot(AudioSource audioSource);
}