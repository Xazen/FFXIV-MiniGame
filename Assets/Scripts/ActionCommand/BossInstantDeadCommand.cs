using UnityEngine;

public class BossInstantDeadCommand : ActionCommand
{
    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.ReduceHp(Mathf.RoundToInt(target.MaxHP * 1.5f));
    }
}
