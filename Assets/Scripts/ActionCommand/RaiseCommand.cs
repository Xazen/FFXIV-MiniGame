using System.Collections;
using UnityEngine;

public class RaiseCommand : PlayerActionCommand
{
    [SerializeField]
    private float _healValue;

    [SerializeField]
    private float _timeUntilDamage;

    [SerializeField]
    private GameObject _effectPrefab;

    private GameObject _effect;

    public override bool CanBeUsed(Actor target)
    {
        return target.IsDead();
    }

    public override void Execute(Actor target)
    {
        base.Execute(target);

        var effectPos = target.transform.position;
        effectPos.z = -1;
        _effect = Instantiate(_effectPrefab, effectPos, Quaternion.identity);
        Target.Raise(Mathf.RoundToInt(Target.MaxHP * _healValue));
        StartCoroutine(CauseDamage(_timeUntilDamage));
    }

    private IEnumerator CauseDamage(float timeUntilDamage)
    {
        yield return new WaitForSeconds(timeUntilDamage);
        Destroy(_effect);
    }
}