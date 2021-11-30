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

    private void Start()
    {
        //_turnCounterSO.TurnCount = 11;
        UpdateWinUI();
        StartCoroutine(UIProgression());
        _horizontalBarFill.fillAmount = 0f;
    }

    private IEnumerator UIProgression()
    {
        float turnCountValue = _ratingSystemSO.RatingTurnCounts[0] + 1f; //to be decreased based on percentage, 1f acts as offset for both the loop and the bar fill
        float maxRatingValue = _ratingSystemSO.RatingTurnCounts[_ratingSystemSO.RatingTurnCounts.Count - 1];
        float minRatingValue = _ratingSystemSO.RatingTurnCounts[0];
        float ratingDifference = minRatingValue - maxRatingValue;

        int starValueIndex = 0;
        while(turnCountValue > _turnCounterSO.TurnCount)
        {
            turnCountValue -= Time.deltaTime * _fillSpeed;
            if (turnCountValue < _ratingSystemSO.RatingTurnCounts[starValueIndex])
            {
                _starHolders[starValueIndex].PlayPopOutAnimation();
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
        for (int i = 0; i < _ratingSystemSO.RatingTurnCounts.Count; i++)
        {
            _turnTexts[i].text = _ratingSystemSO.RatingTurnCounts[i].ToString("00");
        }

    }
}
