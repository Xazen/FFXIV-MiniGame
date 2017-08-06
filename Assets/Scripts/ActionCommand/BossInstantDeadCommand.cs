using System.Collections;
using UnityEngine;

public class BossInstantDeadCommand : ActionCommand
{
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
        Target.ReduceHp(Mathf.RoundToInt(Target.MaxHP * 1.5f));
        Destroy(_effect);
    }
}
