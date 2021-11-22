using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOverPanel : MonoBehaviour
{
    [SerializeField] GameObject _UIParent;

    private void Start()
    {
        GameManager.Instance.OnGameEnd += ShowGameOverPanel;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameEnd -= ShowGameOverPanel;
    }

    private void ShowGameOverPanel()
    {
        _UIParent.SetActive(true);
    }
}
