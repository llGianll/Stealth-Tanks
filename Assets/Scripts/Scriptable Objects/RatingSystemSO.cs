using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Level/Rating System")]
public class RatingSystemSO : ScriptableObject
{
    [SerializeField] List<int> _ratingTurnCounts = new List<int>();

    public List<int> RatingTurnCounts => _ratingTurnCounts;
}
