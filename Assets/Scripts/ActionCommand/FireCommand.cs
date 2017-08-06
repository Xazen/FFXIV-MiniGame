using System;
using DG.Tweening;
using UnityEngine;

public class FireCommand : ActionCommand
{
    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private GameObject _firePrefab;

    private GameObject _instantiatedFire;

    public override void Execute(Actor target)
    {
        base.Execute(target);
        _instantiatedFire = Instantiate(_firePrefab, transform.position, Quaternion.identity);
        _instantiatedFire.transform.DOMove(GameSystem.Instance().Hydra.transform.position, 0.6f).OnComplete(OnComplete);        
    }

    private void OnComplete()
    {
        if (_instantiatedFire != null)
        {
            Destroy(_instantiatedFire);
        }
        Target.ReduceHp(_attackDamage);
    }
}
