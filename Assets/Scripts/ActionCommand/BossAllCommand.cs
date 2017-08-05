using UnityEngine;

public class BossAllCommand : ActionCommand
{
    [SerializeField]
    private int _attackDamage;

    public override void Execute(Actor target)
    {
        base.Execute(target);
        GameSystem gameSystem = GameSystem.Instance();
        gameSystem.Tank.ReduceHp(_attackDamage);
        gameSystem.MeleeDps.ReduceHp(_attackDamage);
        gameSystem.BlackMage.ReduceHp(_attackDamage);
        gameSystem.Player.ReduceHp(_attackDamage);
    }
}