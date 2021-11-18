using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTargeting : MonoBehaviour
{
    //Change Orientation(horizontal, vertical) by pressing tab,
    //get tile collider through MouseTarget class event, then check all adjacent tiles by using the component AdjacentTiles
    //for each adjacent tile, depending on the orientation(horizontal - check left/right , vertical - check up/down) recursively set IsSelected of GridTileProcessor to true
    //GridTileProcessor needs to evaluate if it's in-line or not? 
}
