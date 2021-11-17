using UnityEngine;

[CreateAssetMenu(menuName = "Grid Size")]
public class GridSizeSO : ScriptableObject
{
    [SerializeField] [Range(1, 20)] int _xCount = 5;
    [SerializeField] [Range(1, 20)] int _zCount = 5;

    public int XCount { get { return _xCount; } }
    public int ZCount { get { return _zCount; } }
}
