using UnityEngine;

public class BossAoeCommand : ActionCommand
{
    [SerializeField]
    private int _attackDamage;

    public override void Execute(Actor target)
    {
        base.Execute(target);
        GameSystem gameSystem = GameSystem.Instance();

        if (target == gameSystem.Tank)
        {
            gameSystem.Tank.ReduceHp(_attackDamage);
            gameSystem.MeleeDps.ReduceHp(_attackDamage);
        }
        else if (target == gameSystem.MeleeDps)
        {
            gameSystem.Tank.ReduceHp(_attackDamage);
            gameSystem.MeleeDps.ReduceHp(_attackDamage);
            gameSystem.BlackMage.ReduceHp(_attackDamage);
        }
        else if (target == gameSystem.BlackMage)
        {
            gameSystem.BlackMage.ReduceHp(_attackDamage);
            gameSystem.MeleeDps.ReduceHp(_attackDamage);
        }
        else if (target == gameSystem.Player)
        {
            gameSystem.Player.ReduceHp(_attackDamage);
        }
    }
}