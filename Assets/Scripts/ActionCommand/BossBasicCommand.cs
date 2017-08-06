using System;
using System.Collections;
using UnityEngine;

public class BossBasicCommand : ActionCommand
{
    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private float _paralyzedChance;

    [SerializeField]
    private float _timeUntilDamage;

    [SerializeField]
    private GameObject _effectPrefab;

    private GameObject _effect;

    public override void Execute(Actor target)
    {
        base.Execute(target);

        var effectPos = target.transform.position;
        effectPos.z = -1;
        _effect = Instantiate(_effectPrefab, effectPos, Quaternion.identity);
        StartCoroutine(CauseDamage(_timeUntilDamage));
    }

    private IEnumerator CauseDamage(float timeUntilDamage)
    {
        yield return new WaitForSeconds(timeUntilDamage);
        Target.ReduceHp(_attackDamage);
        TryCausingParalysis(Target);
        Destroy(_effect);
    }

    private void TryCausingParalysis(Actor target)
    {
        float paralyzeChance = UnityEngine.Random.Range(0f, 1f);
        if (paralyzeChance > _paralyzedChance ||
            target == GameSystem.Instance().Player)
        {
            return;
        }

        target.CauseParalysis();
    }
}