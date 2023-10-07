public class SingleTileTargeting : Targeting
{
    protected override void Start() => base.Start();
    protected override void RefreshTargeting()
    {
        if (Target.Count <= 0)
            return;

        Target[0].IsTargeted = false;
        Target.Clear();
    }

    public override void AddTarget()
    {
        GridTileProcessor target = MouseTarget.Instance.HitCollider.GetComponent<GridTileProcessor>();
        if (target == null)
            return;

        Target.Add(target);
        Target[0].IsTargeted = true;
    }
}
