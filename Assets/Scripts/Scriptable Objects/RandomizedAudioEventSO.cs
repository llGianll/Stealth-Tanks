using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioEvent/Randomized", fileName = "AudioEvent")]
public class RandomizedAudioEventSO : AudioEventSO
{
    [SerializeField] List<AudioClip> _audioClips = new List<AudioClip>();
    
    [Header("Volume Randomization Range")]
    [SerializeField] RangedFloat _volume;
    
    [Header("Pitch Randomization Range")]
    [SerializeField] RangedFloat _pitch;

    public override void Play(AudioSource audioSource)    
    {
        if (_audioClips.Count <= 0)
            return;

        RandomizeAudioElements(audioSource);
        audioSource.Play();
    }

    public override void PlayOneShot(AudioSource audioSource)
    {
        if (_audioClips.Count <= 0)
            return;

        RandomizeAudioElements(audioSource);
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void RandomizeAudioElements(AudioSource audioSource)
    {
        audioSource.clip = _audioClips[Random.Range(0, _audioClips.Count)];
        audioSource.volume = Random.Range(_volume.minValue, _volume.maxValue);
        audioSource.pitch = Random.Range(_pitch.minValue, _pitch.maxValue);
    }
}

[System.Serializable]
public class RangedFloat
{
    [Range(0, 5)] public float minValue = 0f;
    [Range(0, 5)] public float maxValue = 1f;

}
