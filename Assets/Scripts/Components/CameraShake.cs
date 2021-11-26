using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float _duration = 0.5f;
    [SerializeField] float _intensity = 1f;
    
    float _shakeTimer;
    
    CinemachineVirtualCamera _vCam;
    CinemachineBasicMultiChannelPerlin _perlin;

    public static CameraShake Instance;

    private void Awake()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
        _perlin = _vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Instance = this;
    }

    private void Start()
    {
        _shakeTimer = 0f;
    }

    public void Shake() 
    {
        _perlin.m_AmplitudeGain = _intensity;
        _shakeTimer = _duration;
    }

    public void Shake(float intensity, float duration)
    {
        _perlin.m_AmplitudeGain = intensity;
        _shakeTimer = duration;
    }

    void Update()
    {
        if(_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0)
                _perlin.m_AmplitudeGain = 0f;
        }
    }
}
