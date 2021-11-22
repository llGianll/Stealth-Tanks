using UnityEngine;

[CreateAssetMenu(menuName = "Level/Turn Counter")]
public class TurnCounterSO : ScriptableObject
{
    [SerializeField] string _levelName;
    int _turnCount;

    public string LevelName => _levelName;
    public int TurnCount
    {
        get { return _turnCount; }
        set { _turnCount = value; }
    }

}
