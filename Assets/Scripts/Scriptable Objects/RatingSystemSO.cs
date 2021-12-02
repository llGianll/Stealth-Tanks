using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Level/Rating System")]
public class RatingSystemSO : ScriptableObject
{
    [SerializeField] List<Rating> _ratingTurnCounts = new List<Rating>();

    public List<Rating> Rating => _ratingTurnCounts;
}

[System.Serializable]
public class Rating
{
    [SerializeField] int _turnCount;
    [SerializeField] AudioEventSO _ratingSfx;

    public int TurnCount => _turnCount;
    public AudioEventSO RatingSFX => _ratingSfx;
}