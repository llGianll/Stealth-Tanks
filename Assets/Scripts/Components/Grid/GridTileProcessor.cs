/*
 * component that listens to mouse input, raycast detection. Other tile elements like enemy, ground subscribes to events of this class and executes their functions 
 */
using System;
using UnityEngine;

public class GridTileProcessor : MonoBehaviour
{
    AdjacentTilesChecker _adjacentChecker;
    bool _isTargeted;

    public bool IsOccupied { get; set; } // value can potentially also just be derived from searching the child objects
    public bool IsClicked { get; set; }
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
        _adjacentChecker = GetComponent<AdjacentTilesChecker>();
    }

    public void Clicked()
    {
        IsClicked = true;
        OnClicked();
    }
}
