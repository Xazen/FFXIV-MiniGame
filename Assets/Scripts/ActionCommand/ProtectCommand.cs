public class ProtectCommand : PlayerActionCommand
{
    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.CastProtect();
    }
}