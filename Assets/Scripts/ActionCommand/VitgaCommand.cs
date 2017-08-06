using System;
using UnityEngine;

public class VitgaCommand : PlayerActionCommand
{
    [SerializeField]
    private int _healValue;

    public override bool CanBeUsed(Actor target)
    {
        return true;
    }

    public override void Execute(Actor target)
    {
        base.Execute(target);
        GameSystem gameSystem = GameSystem.Instance();
        gameSystem.Tank.IncreaseHp(_healValue);
        gameSystem.MeleeDps.IncreaseHp(_healValue);
        gameSystem.BlackMage.IncreaseHp(_healValue);
        gameSystem.Player.IncreaseHp(_healValue);
    }
}