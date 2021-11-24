using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOverPanel : MonoBehaviour
{
    [SerializeField] GameObject _winUIParent;
    [SerializeField] GameObject _loseUIParent;

    private void Start()
    {
        GameManager.Instance.OnGameEnd += ShowGameOverPanel;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameEnd -= ShowGameOverPanel;
    }

    private void ShowGameOverPanel(bool win)
    {
        if (win)
            _winUIParent.SetActive(true);
        else
            _loseUIParent.SetActive(true);
    }
}
