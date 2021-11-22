using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] RectTransform _segmentLayout;
    [SerializeField] GameObject _healthSegmentPrefab;
    [SerializeField] Color _healthFull;
    [SerializeField] Color _healthEmpty;

    public EnemyUnit Unit { get; set; } //[refactor] too rigid, only allows Enemy 

    List<Image> _healthSegments = new List<Image>();
    bool isInitialized = false;

    private void OnEnable()
    {
        if (Unit != null)
        {
            Unit.OnHealthUpdate += UpdateHealthBar;
            Debug.Log("ENABLE HEALTH");
        }
    }

    private void OnDisable()
    {
        //if (Unit != null)
        //{
        //    Unit.OnHealthUpdate -= UpdateHealthBar;
        //    Debug.Log("Disable Health");

        //}
    }

    private void InitializeHealthBar(float maxHealth)
    {
        Debug.Log(maxHealth);
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject healthSegment = Instantiate(_healthSegmentPrefab, _segmentLayout);
            _healthSegments.Add(healthSegment.GetComponent<Image>());
        }

        foreach (var item in _healthSegments)
        {
            item.color = _healthFull;
        }


        isInitialized = true;
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (!isInitialized)
            InitializeHealthBar(maxHealth);

        for (int i = 0; i < maxHealth; i++)
        {
            _healthSegments[i].color = _healthEmpty;
        }

        for (int i = 0; i < currentHealth; i++)
        {
            _healthSegments[i].color = _healthFull;
        }
    }
}
