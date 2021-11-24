using UnityEngine;
using TMPro;
using System;

public class UITurnPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _turnText;
    [SerializeField] TextMeshProUGUI _turnsToClearText;
    [SerializeField] TurnCounterSO _turnCounterSO;

    private void Start()
    {
        TurnManager.Instance.OnCurrentTurnChange += UpdateTurnText;
        _turnsToClearText.text = "Destroy all enemies within " + _turnCounterSO.TurnsToClear + " turns";
    }

    private void OnDisable()
    {
        TurnManager.Instance.OnCurrentTurnChange -= UpdateTurnText;
    }
    private void UpdateTurnText(int turnCount)
    {
        _turnText.text = "Turn " + turnCount;
    }

}
