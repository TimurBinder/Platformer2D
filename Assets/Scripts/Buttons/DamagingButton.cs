public class DamagingButton : ButtonListener
{
    protected override void Attack()
    {
        Health.Reduce(Value);
    }
}
