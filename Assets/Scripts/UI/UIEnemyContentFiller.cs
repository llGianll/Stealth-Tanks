using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyContentFiller : MonoBehaviour
{
    [SerializeField] EnemySpawnListSO _enemySpawnList;
    [SerializeField] UIEnemyContent _enemyContentPrefab;
    List<UIEnemyContent> _enemyContentList = new List<UIEnemyContent>();

    private void Start()
    {
        foreach (var enemyType in _enemySpawnList.EnemySpawnListData)
        {
            GameObject _enemyUI = Instantiate(_enemyContentPrefab.gameObject, transform.position, Quaternion.identity);
            _enemyUI.transform.parent = this.transform;
            _enemyUI.GetComponent<UIEnemyContent>().SetEnemyIcon(enemyType.EnemyIcon);
            _enemyUI.GetComponent<UIEnemyContent>().SetEnemyCount(enemyType.Count);
            _enemyUI.GetComponent<UIEnemyContent>().SetID(enemyType.ID);
        }
    }
}
