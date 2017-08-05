using UnityEngine;

public class PartyMember : Actor
{
    [Header("Party Member")]
    [SerializeField]
    private Boss _attackTarget;

    [SerializeField]
    private ActionCommand _autoAttack;

    [SerializeField]
    private float _attackFrequency;

    private float _timeUntilNextAttack;

    protected override void Update()
    {
        base.Update();

        TryAttack();
    }

    private void TryAttack()
    {
        _timeUntilNextAttack -= Time.deltaTime;
        if (_timeUntilNextAttack <= 0)
        {
            ExectueAction(_autoAttack, _attackTarget);
            _timeUntilNextAttack = _attackFrequency;
        }
    }
}