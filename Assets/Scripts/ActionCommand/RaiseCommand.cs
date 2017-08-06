using UnityEngine;

public class RaiseCommand : PlayerActionCommand
{
    [SerializeField]
    private int _healValue;

    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.Raise(_healValue);
    }
}