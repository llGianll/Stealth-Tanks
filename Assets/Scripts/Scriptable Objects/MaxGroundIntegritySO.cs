using UnityEngine;

[CreateAssetMenu(menuName = "Level/Max Ground Integrity")]
public class MaxGroundIntegritySO : ScriptableObject
{
    [SerializeField] [Range(1, 5)] int _integrity = 5;

    public int Integrity { get { return _integrity; } }
}
