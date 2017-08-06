using UnityEngine;

public class Boss : Actor
{
    [Header("Boss")]
    [SerializeField]
    private float _attackFrequency;

    [SerializeField]
    private BossAttack _basicAttack;

    [SerializeField]
    private BossAttack _aoeAttack;

    [SerializeField]
    private BossAttack _allAttack;

    [SerializeField]
    private BossAttack _instantDeadAttack;

    private BossAttack[] _bossAttacks = new BossAttack[4];
    private float _timeUntilNextAttack;
    private BossAttack _priorityAttack;

    public override void Start()
    {
        base.Start();
        _timeUntilNextAttack = _attackFrequency;
        _bossAttacks[0] = _basicAttack;
        _bossAttacks[1] = _aoeAttack;
        _bossAttacks[2] = _allAttack;
        _bossAttacks[3] = _instantDeadAttack;
    }

    protected override void Update()
    {
        base.Update();
        if (IsDead())
        {
            return;
        }
        TryAttack();
        OnHpChangedDelegate += OnHpChanged;
    }

    private void OnHpChanged(Actor actor, int oldValue, int newValue)
    {
        float oldPercentage = (float) oldValue / actor.MaxHP;
        float newPercentage = (float) newValue / actor.MaxHP;

        for (int i = 0; i < _bossAttacks.Length; i++)
        {
            BossAttack bossAttack = _bossAttacks[i];
            if (oldPercentage > bossAttack.HpTrigger &&
                newPercentage < bossAttack.HpTrigger)
            {
                _priorityAttack = bossAttack;
                break;
            }
        }
    }

    private void TryAttack()
    {
        _timeUntilNextAttack -= Time.deltaTime;
        if (_timeUntilNextAttack <= 0)
        {
            ExecuteAttack();
            _timeUntilNextAttack = _attackFrequency;
        }
    }

    private void ExecuteAttack()
    {
        if (_priorityAttack != null)
        {
            _priorityAttack.Execute();
            _priorityAttack = null;
            return;
        }

        float hpPercentage = (float) CurrentHP / MaxHP;
        int maxRandomValue = 0;
        for (int i = 0; i < _bossAttacks.Length; i++)
        {
            BossAttack bossAttack = _bossAttacks[i];
            if (bossAttack.HpTrigger >= hpPercentage)
            {
                maxRandomValue += bossAttack.ProbabilityWeight;
            }
        }

        int randomValue = Random.Range(0, maxRandomValue);
        for (int i = 0; i < _bossAttacks.Length; i++)
        {
            BossAttack bossAttack = _bossAttacks[i];
            int attackProbability = 0;
            for (int j = 0; j < i + 1; j++)
            {
                attackProbability += _bossAttacks[j].ProbabilityWeight;
            }

            if (randomValue <= attackProbability)
            {
                bossAttack.Execute();
                return;
            }
        }
    }
}