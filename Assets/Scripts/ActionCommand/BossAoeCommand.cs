using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossAoeCommand : ActionCommand
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
        var meleePos = gameSystem.MeleeDps.transform.position;
        meleePos.z = -1;
        var magePos = gameSystem.BlackMage.transform.position;
        magePos.z = -1;
        var playerPos = gameSystem.Player.transform.position;
        playerPos.z = -1;
        if (target == gameSystem.Tank)
        {
            _effect.Add(Instantiate(_effectPrefab, tankPos, Quaternion.identity));
            _effect.Add(Instantiate(_effectPrefab, meleePos, Quaternion.identity));
        }
        else if (target == gameSystem.MeleeDps)
        {
            _effect.Add(Instantiate(_effectPrefab, tankPos, Quaternion.identity));
            _effect.Add(Instantiate(_effectPrefab, meleePos, Quaternion.identity));
            _effect.Add(Instantiate(_effectPrefab, magePos, Quaternion.identity));            
        }
        else if (target == gameSystem.BlackMage)
        {
            _effect.Add(Instantiate(_effectPrefab, meleePos, Quaternion.identity));
            _effect.Add(Instantiate(_effectPrefab, magePos, Quaternion.identity));
        }
        else if (target == gameSystem.Player)
        {
            _effect.Add(Instantiate(_effectPrefab, playerPos, Quaternion.identity));
        }
        Camera.main.DOShakePosition(0.5f, 0.325f, 50);
        StartCoroutine(CauseDamage(_timeUntilDamage));
    }

    private IEnumerator CauseDamage(float timeUntilDamage)
    {
        yield return new WaitForSeconds(timeUntilDamage);
        GameSystem gameSystem = GameSystem.Instance();

        if (Target == gameSystem.Tank)
        {
            gameSystem.Tank.ReduceHp(_attackDamage);
            gameSystem.MeleeDps.ReduceHp(_attackDamage);
        }
        else if (Target == gameSystem.MeleeDps)
        {
            gameSystem.Tank.ReduceHp(_attackDamage);
            gameSystem.MeleeDps.ReduceHp(_attackDamage);
            gameSystem.BlackMage.ReduceHp(_attackDamage);
        }
        else if (Target == gameSystem.BlackMage)
        {
            gameSystem.BlackMage.ReduceHp(_attackDamage);
            gameSystem.MeleeDps.ReduceHp(_attackDamage);
        }
        else if (Target == gameSystem.Player)
        {
            gameSystem.Player.ReduceHp(_attackDamage);
        }

        for (int i = 0; i < _effect.Count; i++)
        {
            Destroy(_effect[i]);
        }
        _effect.Clear();
    }
}