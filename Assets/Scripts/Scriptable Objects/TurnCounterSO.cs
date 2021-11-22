using UnityEngine;

[CreateAssetMenu(menuName = "Level/Turn Counter")]
public class TurnCounterSO : ScriptableObject
{
    int _turnCount;

    public int TurnCount
    {
        get { return _turnCount; }
        set { _turnCount = value; }
    }
}
