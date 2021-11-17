/*
 * component that listens to mouse input, raycast detection. Other tile elements like enemy, ground subscribes to events of this class and executes their functions 
 */
using System;
using UnityEngine;

public class GridTileProcessor : MonoBehaviour
{
    public Action OnMouseHover = delegate { };
    public Action OnClicked = delegate { };
}
