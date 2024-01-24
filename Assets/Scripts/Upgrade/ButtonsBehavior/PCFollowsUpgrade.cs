public class PCFollows : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();
        PC.IsFollowing = true;
    }
}
