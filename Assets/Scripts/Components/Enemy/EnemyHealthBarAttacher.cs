using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarAttacher : MonoBehaviour
{
    [SerializeField] GameObject _healthBarPrefab;
    [SerializeField] Transform _healthBarAnchor;

    UnitGridPlacement _enemyGridPlacement;
    EnemyUnit _enemyUnit;

    private void Awake()
    {
        _enemyGridPlacement = GetComponent<UnitGridPlacement>();
        _enemyUnit = GetComponent<EnemyUnit>();
    }

    private void OnEnable()
    {
        _enemyGridPlacement.OnFinishedPlacement += AttachHealthBar;
    }

    private void OnDisable()
    {
        _enemyGridPlacement.OnFinishedPlacement -= AttachHealthBar;
    }

    private void AttachHealthBar(List<GridTileProcessor> gridTiles)
    {
        GameObject healthBar = Instantiate(_healthBarPrefab, _healthBarPrefab.transform.position, _healthBarPrefab.transform.rotation);
        healthBar.GetComponent<RectTransform>().parent = _healthBarAnchor;
        healthBar.GetComponent<RectTransform>().localPosition = Vector3.zero;
        healthBar.GetComponent<UIHealthBar>().Unit = transform.GetComponent<EnemyUnit>();
    }
}
