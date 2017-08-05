using UnityEngine;

public class QuickSwordCommand : ActionCommand
{
    [SerializeField]
    private int _attackDamage;

    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.ReduceHp(_attackDamage);
    }
}
