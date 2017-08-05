using UnityEngine;

public class BossInstantDeadCommand : ActionCommand
{
    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.ReduceHp(target.MaxHP);
    }
}
