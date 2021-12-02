using TMPro;
using UnityEngine;

public class UIGameStart : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] Animator _animator;

    [Header("Scriptable Object References")]
    [SerializeField] TurnCounterSO _turnCounterSO;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI _levelBannerText;
    [SerializeField] TextMeshProUGUI _startGameText;

    [Header("Sound")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioEventSO _sfx_SwishEnter;
    [SerializeField] AudioEventSO _sfx_SwishExit;

    private void Start()
    {
        _levelBannerText.text = _turnCounterSO.LevelName;
        _startGameText.text = "Reveal and defeat all enemies within <color=#ff0000ff>" + _turnCounterSO.TurnsToClear + " turns. </color>";
        _animator.SetTrigger("Move");
    }

    public void SFXSwishEnter()
    {
        _sfx_SwishEnter.Play(_audioSource);
    }

    public void SFXSwishExit()
    {
        _sfx_SwishExit.Play(_audioSource);
    }
}
