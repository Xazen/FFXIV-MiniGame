public class ProtectCommand : PlayerActionCommand
{
    public override bool CanBeUsed(Actor target)
    {
        return !target.IsDead() && !target.IsProtectEnabled();
    }
    
    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.CastProtect();
    }
}