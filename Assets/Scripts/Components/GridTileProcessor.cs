/*
 * component that listens to mouse input, raycast detection. Other tile elements like enemy, ground subscribes to events of this class and executes their functions 
 */
using System;
using UnityEngine;

public class GridTileProcessor : MonoBehaviour
{
    Collider _col;

    public int CellIndex { get; set; }
    public bool IsOccupied { get; set; } // value can potentially also just be derived from searching the child objects
    public bool IsSelected { get; set; }


    public Action OnMouseHover = delegate { };
    public Action OnClicked = delegate { };

    private void Awake()
    {
        _col = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        MouseTarget.Instance.OnChangeTarget += ProcessHoverFeedback;
    }

    private void OnDisable()
    {
        MouseTarget.Instance.OnChangeTarget -= ProcessHoverFeedback;
    }

    private void ProcessHoverFeedback(Collider col)
    {
        IsSelected = (_col == col) ? true : false;

        OnMouseHover();
    }

    public void Clicked()
    {
        OnClicked();
    }
}
