using System;
using System.Collections;
using UnityEngine;

public class VitgaCommand : PlayerActionCommand
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
        return true;
    }

    public override void Execute(Actor target)
    {
        base.Execute(target);

        _effect = Instantiate(_effectPrefab, new Vector3(0,0,-1), Quaternion.identity);
        GameSystem gameSystem = GameSystem.Instance();
        gameSystem.Tank.IncreaseHp(_healValue);
        gameSystem.MeleeDps.IncreaseHp(_healValue);
        gameSystem.BlackMage.IncreaseHp(_healValue);
        gameSystem.Player.IncreaseHp(_healValue);
        StartCoroutine(CauseDamage(_timeUntilDamage));
    }

    private IEnumerator CauseDamage(float timeUntilDamage)
    {
        yield return new WaitForSeconds(timeUntilDamage);
        Destroy(_effect);
    }
}