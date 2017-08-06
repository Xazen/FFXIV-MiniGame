using UnityEngine;

public class BossBasicCommand : ActionCommand
{
    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private float _paralyzedChance;

    public override void Execute(Actor target)
    {
        base.Execute(target);
        target.ReduceHp(_attackDamage);

        TryCausingParalysis(target);
    }

    private void TryCausingParalysis(Actor target)
    {
        float paralyzeChance = Random.Range(0f, 1f);
        if (paralyzeChance > _paralyzedChance)
        {
            return;
        }

        target.CauseParalysis();
    }
}