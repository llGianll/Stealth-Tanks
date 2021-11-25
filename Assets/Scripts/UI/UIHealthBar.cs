using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RectTransform _segmentLayout;
    [SerializeField] GameObject _healthSegmentPrefab;
    [Header("Default HP Colors")]
    [SerializeField] Color _healthFull;
    [SerializeField] Color _healthEmpty;
    [Header("Damage Color and Bounce Effects")]
    [SerializeField] AnimationCurve _effectsCurve;
    [SerializeField] Color _damagedColor;
    [SerializeField] float _flashDurationInSecs = 1f;
    [SerializeField] float _minBounceScale = 0.75f;


    public EnemyUnit Unit { get; set; } //[refactor] too rigid, only allows Enemy 

    List<Image> _healthSegments = new List<Image>();
    bool isInitialized = false;

    private void OnEnable()
    {
        if (Unit != null)
        {
            Unit.OnHealthUpdate += UpdateHealthBar;
        }
    }

    private void InitializeHealthBar(float maxHealth)
    {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject healthSegment = Instantiate(_healthSegmentPrefab, _segmentLayout);
            _healthSegments.Add(healthSegment.transform.GetChild(0).GetComponent<Image>());
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

        if(currentHealth < maxHealth)
        {
            int damagedSegmentIndex = (int)currentHealth;
            //_healthSegments[damagedSegmentIndex].color = _healthEmpty;
            StartCoroutine(DamageFlash(_healthSegments[damagedSegmentIndex]));
        }
           
    }

    private IEnumerator DamageFlash(Image healthSegment)
    {
        float alpha = 1;
        float scale = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < _flashDurationInSecs)
        {
            elapsedTime += Time.deltaTime;
            //alpha = Mathf.Lerp(1f, 0f, elapsedTime / _flashDurationInSecs);
            float percent = elapsedTime / _flashDurationInSecs;

            alpha = Mathf.Lerp(1f, 0f, _effectsCurve.Evaluate(percent));
            scale = Mathf.Lerp(0.75f, 1f, _effectsCurve.Evaluate(percent));

            healthSegment.color = new Color(_damagedColor.r, _damagedColor.g, _damagedColor.b, alpha);
            healthSegment.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
    }
}
