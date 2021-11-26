using UnityEngine;

[CreateAssetMenu(menuName = "Level/Turn Counter")]
public class TurnCounterSO : ScriptableObject
{
    [SerializeField] string _levelName;
    [SerializeField] int _turnsToClear;
    [Header("End Screen Use")]

    [SerializeField] [TextArea(3, 5)] string _failMessage;
    [SerializeField] [TextArea(3, 5)] string _successMessage;

    int _turnCount;
    string _endScreenMessage;

    public string LevelName => _levelName;
    public int TurnCount
    {
        get { return _turnCount; }
        set { _turnCount = value; }
    }

    public int TurnsToClear => _turnsToClear;
    public string EndScreenMessage 
    { 
        get 
        {
            if (_turnCount > _turnsToClear)
                _endScreenMessage = _failMessage;
            else
                _endScreenMessage = _successMessage;

            return _endScreenMessage;
        }
    }

}
