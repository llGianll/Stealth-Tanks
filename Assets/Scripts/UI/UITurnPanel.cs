using UnityEngine;
using TMPro;
using System;

public class UITurnPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _turnText;

    private void Start()
    {
        TurnManager.Instance.OnCurrentTurnChange += UpdateTurnText;
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
