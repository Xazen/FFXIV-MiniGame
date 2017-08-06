public class EsunaCommand : PlayerActionCommand
{
    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.DisableParalysis();
    }
}