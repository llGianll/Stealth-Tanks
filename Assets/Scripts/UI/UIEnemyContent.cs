using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyContent : MonoBehaviour
{
    [SerializeField] Image _enemyIcon;
    [SerializeField] TextMeshProUGUI _enemyCountText;

    string _enemyID;


    private void Start()
    {
        GameManager.Instance.OnEnemyCountUpdate += UpdateCount;
    }

    private void UpdateCount(List<EnemySpawnData> _enemyList, string id)
    {
        foreach (var enemy in _enemyList)
        {
            if (enemy.ID == _enemyID)
                _enemyCountText.text = enemy.Count.ToString();
        }
    }

    public void SetEnemyIcon(Sprite icon)
    {
        _enemyIcon.sprite = icon;
    }

    public void SetEnemyCount(int enemyCount)
    {
        _enemyCountText.text = enemyCount.ToString();
    }

    public void SetID(string id)
    {
        _enemyID = id;
    }
}
