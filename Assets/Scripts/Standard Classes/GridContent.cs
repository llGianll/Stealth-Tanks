using UnityEngine;

[System.Serializable]
public class GridContent
{
    public GameObject _groundTile;
    public int _cellIndex;
    public bool Occupied { get; set; }

    public GridContent(GameObject groundTile, int index)
    {
        _groundTile = groundTile;
        _cellIndex = index;
    }
}
