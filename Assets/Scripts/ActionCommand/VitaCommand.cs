using System.Collections;
using UnityEngine;

public class VitaCommand : PlayerActionCommand
{
    [SerializeField]
    private int _healValue;

    [SerializeField]
    private float _timeUntilDamage;

    [SerializeField]
    private GameObject _effectPrefab;

    private GameObject _effect;

    public override bool CanBeUsed(Actor target)
    {
        return (!target.IsDead() && target.CurrentHP < target.MaxHP);
    }

    public override void Execute(Actor target)
    {
        base.Execute(target);

        var effectPos = target.transform.position;
        effectPos.z = -1;
        _effect = Instantiate(_effectPrefab, effectPos, Quaternion.identity);
        Target.IncreaseHp(_healValue);
        StartCoroutine(CauseDamage(_timeUntilDamage));
    }

    private IEnumerator CauseDamage(float timeUntilDamage)
    {
        yield return new WaitForSeconds(timeUntilDamage);
        Destroy(_effect);
    }
}