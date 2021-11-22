using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWinPanel : MonoBehaviour
{
    [SerializeField] TurnCounterSO _turnCounterSO;
    [SerializeField] RatingSystemSO _ratingSystemSO;
    [SerializeField] TextMeshProUGUI _levelNameText;
    [SerializeField] Sprite _starFull;
    [SerializeField] Sprite _starEmpty;
    [SerializeField] List<Image> _starImages = new List<Image>();
    [SerializeField] List<TextMeshProUGUI> _turnTexts = new List<TextMeshProUGUI>();
    [SerializeField] TextMeshProUGUI _turnsCleared;

    private void Start()
    {
        UpdateWinUI();
    }

    private void UpdateWinUI()
    {
        _levelNameText.text = _turnCounterSO.LevelName;
        _turnsCleared.text = "Turns: "+_turnCounterSO.TurnCount;

        _turnTexts[0].text = _ratingSystemSO.oneStar.ToString();
        _turnTexts[1].text = _ratingSystemSO.twoStar.ToString();
        _turnTexts[2].text = _ratingSystemSO.threeStar.ToString();

        _starImages[0].sprite = (_turnCounterSO.TurnCount <= _ratingSystemSO.oneStar)   ? _starFull : _starEmpty;
        _starImages[1].sprite = (_turnCounterSO.TurnCount <= _ratingSystemSO.twoStar)   ? _starFull : _starEmpty;
        _starImages[2].sprite = (_turnCounterSO.TurnCount <= _ratingSystemSO.threeStar) ? _starFull : _starEmpty;
    }
}
