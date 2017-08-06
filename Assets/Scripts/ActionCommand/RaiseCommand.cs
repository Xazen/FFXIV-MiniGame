using System;
using UnityEngine;

public class RaiseCommand : PlayerActionCommand
{
    [SerializeField]
    private float _healValue;

    public override bool CanBeUsed(Actor target)
    {
        return target.IsDead();
    }

    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.Raise(Mathf.RoundToInt(target.MaxHP * _healValue));
    }
}