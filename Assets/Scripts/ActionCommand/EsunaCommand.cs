public class EsunaCommand : PlayerActionCommand
{
    public override bool CanBeUsed(Actor target)
    {
        return target.IsParalyzed;
    }

    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.DisableParalysis();
    }
}