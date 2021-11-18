/*
 * component that listens to mouse input, raycast detection. Other tile elements like enemy, ground subscribes to events of this class and executes their functions 
 */
using System;
using UnityEngine;

public class GridTileProcessor : MonoBehaviour
{
    Collider _col;
    AdjacentTilesChecker _adjacentChecker;
    bool _isTargeted;

    public int CellIndex { get; set; }
    public bool IsOccupied { get; set; } // value can potentially also just be derived from searching the child objects
    public bool IsTargeted 
    {
        get { return _isTargeted; } 
        set 
        { 
            _isTargeted = value;
            OnSelectionChange();
        } 
    }

    public AdjacentTilesChecker AdjacentChecker => _adjacentChecker;

    //public Action OnMouseHover = delegate { };
    public Action OnSelectionChange = delegate { };
    public Action OnClicked = delegate { };

    private void Awake()
    {
        //_col = GetComponent<Collider>();
        _adjacentChecker = GetComponent<AdjacentTilesChecker>();
    }

    private void OnEnable()
    {
        //MouseTarget.Instance.OnChangeTarget += ProcessHoverFeedback;
    }

    private void OnDisable()
    {
        //MouseTarget.Instance.OnChangeTarget -= ProcessHoverFeedback;
    }

    //private void ProcessHoverFeedback(Collider col)
    //{
    //    OnMouseHover();
    //}

    public void Clicked()
    {
        OnClicked();
    }
}
