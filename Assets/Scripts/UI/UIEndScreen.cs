using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndScreen : MonoBehaviour
{
    [Header("Scriptable Object References")]
    [SerializeField] TurnCounterSO _turnCounterSO;
    [SerializeField] RatingSystemSO _ratingSystemSO;
    [SerializeField] Animator _animator;
    [Header("UI References")]
    [SerializeField] TextMeshProUGUI _levelNameText;
    [SerializeField] TextMeshProUGUI _endScreenMessage;
    [SerializeField] TextMeshProUGUI _turnsCleared;
    [SerializeField] List<UIStarHolder> _starHolders = new List<UIStarHolder>();
    [SerializeField] List<TextMeshProUGUI> _turnTexts = new List<TextMeshProUGUI>();
    [Header("Horizontal Fill")]
    [SerializeField] float _fillSpeed = 5f;
    [SerializeField] Image _horizontalBarFill;
    [Header("Audio")]
    [SerializeField] AudioSource _audioSource;

    private void Start()
    {
        UpdateWinUI();
        StartCoroutine(UIProgression());
        _horizontalBarFill.fillAmount = 0f;
    }

    private IEnumerator UIProgression()
    {
        float turnCountValue = _ratingSystemSO.Rating[0].TurnCount + 1f; //to be decreased based on percentage, 1f acts as offset for both the loop and the bar fill
        float maxRatingValue = _ratingSystemSO.Rating[_ratingSystemSO.Rating.Count - 1].TurnCount;
        float minRatingValue = _ratingSystemSO.Rating[0].TurnCount;
        float ratingDifference = minRatingValue - maxRatingValue;

        int starValueIndex = 0;
        while(turnCountValue > _turnCounterSO.TurnCount)
        {
            turnCountValue -= Time.deltaTime * _fillSpeed;
            if (turnCountValue < _ratingSystemSO.Rating[starValueIndex].TurnCount)
            {
                _starHolders[starValueIndex].PlayPopOutAnimation(_ratingSystemSO.Rating[starValueIndex].RatingSFX);
                
                starValueIndex++;
            }
        
            float percent = 1- ((turnCountValue - maxRatingValue) / ratingDifference);
            _horizontalBarFill.fillAmount = percent;
            //Debug.Log(percent);

            yield return null;
        }
    }

    private void UpdateWinUI()
    {
        _levelNameText.text = _turnCounterSO.LevelName;
        _endScreenMessage.text = _turnCounterSO.EndScreenMessage;
        _turnsCleared.text = "Turns: "+_turnCounterSO.TurnCount;

        _animator.SetTrigger("Pop");

        //update horizontal bar turn requirement info 
        for (int i = 0; i < _ratingSystemSO.Rating.Count; i++)
        {
            _turnTexts[i].text = _ratingSystemSO.Rating[i].TurnCount.ToString("00");
        }

    }
}
