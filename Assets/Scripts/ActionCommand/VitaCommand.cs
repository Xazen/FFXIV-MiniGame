﻿using System;
using UnityEngine;

public class VitaCommand : PlayerActionCommand
{
    [SerializeField]
    private int _healValue;

    public override bool CanBeUsed(Actor target)
    {
        return (!target.IsDead());
    }

    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.IncreaseHp(_healValue);
    }
}