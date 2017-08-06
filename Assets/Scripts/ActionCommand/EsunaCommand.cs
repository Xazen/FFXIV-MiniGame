using UnityEngine;

public class EsunaCommand : PlayerActionCommand
{
    [SerializeField]
    private int _healValue;

    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.DisableParalysis();
    }
}