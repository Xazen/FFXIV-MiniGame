using UnityEngine;

public class BossBasicCommand : ActionCommand
{
    [SerializeField]
    private int _attackDamage;

    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.ReduceHp(_attackDamage);
    }
}