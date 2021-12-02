using UnityEngine;
using TMPro;
using System;

public class UITurnPanel : MonoBehaviour
{
    [Header("Scriptable Object References")]
    [SerializeField] TurnManager _turnManager;
    [Header("UI References")]
    [SerializeField] TextMeshProUGUI _turnText;
    [SerializeField] TextMeshProUGUI _turnsToClearText;
    [SerializeField] TurnCounterSO _turnCounterSO;

    private void OnEnable()
    {
        _turnManager.OnCurrentTurnChange += UpdateTurnText;
        _turnsToClearText.text = "Destroy all enemies within " + _turnCounterSO.TurnsToClear + " turns";
    }

    private void Start()
    {
        _turnManager.Initialize();
    }

    private void OnDisable()
    {
        _turnManager.OnCurrentTurnChange -= UpdateTurnText;
    }
    private void UpdateTurnText(int turnCount)
    {
        _turnText.text = "Turn " + turnCount;
    }

}
