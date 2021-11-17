/*
 * component that listens to mouse input, raycast detection. Other tile elements like enemy, ground subscribes to events of this class and executes their functions 
 */
using System;
using UnityEngine;

public class GridTileProcessor : MonoBehaviour
{
    public Action<Collider> OnMouseHover = delegate { };
    public Action OnClicked = delegate { };

    public int CellIndex { get; set; }
    public bool IsOccupied { get; set; } // value can potentially also just be derived from searching the child objects

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
        OnMouseHover(col);
    }

    public void Clicked()
    {
        OnClicked();
    }
}
