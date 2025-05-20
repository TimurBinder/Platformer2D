public class HealingButton : ButtonListener
{ 
    protected override void Attack()
    {
        Health.Add(Value);
    }
}
