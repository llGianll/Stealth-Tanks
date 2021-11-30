using System.Collections.Generic;
using UnityEngine;

public interface ITargeting
{
    public void AddTarget(GridTileProcessor target);
    public void RefreshTargeting();
    public void ClickTarget();

    //[refactor] stinky code architecture here, classes that implement ITargeting need to add functions/getters that they don't use 
    public GridTileProcessor TargetTile { get; }
    public List<GridTileProcessor> TargetTiles { get; }
}
