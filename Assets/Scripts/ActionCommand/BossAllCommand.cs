using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAllCommand : ActionCommand
{
    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private float _timeUntilDamage;

    [SerializeField]
    private GameObject _effectPrefab;

    private List<GameObject> _effect = new List<GameObject>();

    public override void Execute(Actor target)
    {
        base.Execute(target);

        GameSystem gameSystem = GameSystem.Instance();

        var tankPos = gameSystem.Tank.transform.position;
        tankPos.z = -1;
        _effect.Add(Instantiate(_effectPrefab, tankPos, Quaternion.identity));

        var meleePos = gameSystem.MeleeDps.transform.position;
        meleePos.z = -1;
        _effect.Add(Instantiate(_effectPrefab, meleePos, Quaternion.identity));

        var magePos = gameSystem.BlackMage.transform.position;
        magePos.z = -1;
        _effect.Add(Instantiate(_effectPrefab, magePos, Quaternion.identity));

        var playerPos = gameSystem.Player.transform.position;
        playerPos.z = -1;
        _effect.Add(Instantiate(_effectPrefab, playerPos, Quaternion.identity));

        StartCoroutine(CauseDamage(_timeUntilDamage));
    }

    private IEnumerator CauseDamage(float timeUntilDamage)
    {
        yield return new WaitForSeconds(timeUntilDamage);
        GameSystem gameSystem = GameSystem.Instance();
        gameSystem.Tank.ReduceHp(_attackDamage);
        gameSystem.MeleeDps.ReduceHp(_attackDamage);
        gameSystem.BlackMage.ReduceHp(_attackDamage);
        gameSystem.Player.ReduceHp(_attackDamage);

        for (int i = 0; i < _effect.Count; i++)
        {
            Destroy(_effect[i]);
        }
        _effect.Clear();
    }
}