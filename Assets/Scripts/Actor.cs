using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    private readonly int DieAnimatorTrigger = Animator.StringToHash("Die");
    private readonly int ActionAnimatorTrigger = Animator.StringToHash("Attack");
    private readonly int RaiseAnimatorTrigger = Animator.StringToHash("Raise");

    [Header("General")]
    [SerializeField]
    private Animator _animator;

    [Header("HP")]
    [SerializeField]
    private int _maxHp;

    [SerializeField]
    private int _autoHpRegenerateValue;

    [SerializeField]
    private float _autoHpRegenerateFrequency;

    [Header("MP")]
    [SerializeField]
    private int _maxMp;

    [SerializeField]
    private int _autoMpRegenerateValue;

    [SerializeField]
    private float _autoMpRegenerateFrequency;

    private int _currentHp;
    private float _timeUntilNextAutoHpRegeneration;
    
    private int _currentMp;
    private float _timeUntilNextAutoMpRegeneration;
           
    public delegate void HpChangedDelegate(Actor actor, int oldValue, int newValue);
    public event HpChangedDelegate OnHpChangedDelegate;

    public delegate void MpChangedDelegate(Actor actor, int oldValue, int newValue);
    public event MpChangedDelegate OnMpChangedDelegate;

    public delegate void ActorDieDelegate(Actor actor);
    public event ActorDieDelegate OnActorDieDelegate;

    public delegate void ActorRaiseDelegate(Actor actor);
    public event ActorRaiseDelegate OnActorRaiseDelegate;

    public int MaxHP { get { return _maxHp; } }

    public virtual void Start()
    {
        _currentHp = _maxHp;
        _currentMp = _maxMp;
    }

    protected virtual void Update()
    {
        if (IsDead())
        {
            return;
        }

        TryRegenerateHp();
        TryRegenerateMp();
    }

    private void TryRegenerateMp()
    {
        _timeUntilNextAutoMpRegeneration -= Time.deltaTime;
        if (_timeUntilNextAutoMpRegeneration <= 0)
        {
            IncreaseHp(_autoMpRegenerateValue);
            _timeUntilNextAutoMpRegeneration = _autoMpRegenerateFrequency;
        }
    }

    private void TryRegenerateHp()
    {
        _timeUntilNextAutoHpRegeneration -= Time.deltaTime;
        if (_timeUntilNextAutoHpRegeneration <= 0)
        {
            IncreaseHp(_autoHpRegenerateValue);
            _timeUntilNextAutoHpRegeneration = _autoHpRegenerateFrequency;
        }
    }

    public void ReduceHp(int value)
    {
        if (IsDead())
        {
            return;
        }

        int oldValue = _currentHp;
        _currentHp = Mathf.Max(_currentHp - value, 0);

        if (OnHpChangedDelegate != null)
        {
            OnHpChangedDelegate(this, oldValue, _currentHp);
        }

        if (_currentHp <= 0)
        {
            Die();
        }
    }

    public void IncreaseHp(int value)
    {
        if (IsDead())
        {
            return;
        }

        int oldValue = _currentHp;
        _currentHp = Mathf.Min(_currentHp + value, _maxHp);

        if (OnHpChangedDelegate != null)
        {
            OnHpChangedDelegate(this, oldValue, _currentHp);
        }
    }

    public void ReduceMp(int value)
    {
        if (IsDead())
        {
            return;
        }

        int oldValue = _currentMp;
        _currentMp = Mathf.Max(_currentMp - value, 0);

        if (OnMpChangedDelegate != null)
        {
            OnMpChangedDelegate(this, oldValue, _currentHp);
        }
    }

    public void IncreaseMp(int value)
    {
        if (IsDead())
        {
            return;
        }

        int oldValue = _currentMp;
        _currentMp = Mathf.Min(_currentMp + value, _maxMp);

        if (OnMpChangedDelegate != null)
        {
            OnMpChangedDelegate(this, oldValue, _currentMp);
        }
    }

    public bool IsDead()
    {
        return _currentHp <= 0;
    }

    public void Raise(int hp)
    {
        if (!IsDead())
        {
            return;
        }

        _currentHp = hp;
        _animator.SetTrigger(RaiseAnimatorTrigger);

        if (OnHpChangedDelegate != null)
        {
            OnHpChangedDelegate(this, 0, _currentHp);
        }

        if (OnActorRaiseDelegate != null)
        {
            OnActorRaiseDelegate(this);
        }
    }

    public void ExectueAction(ActionCommand attackCommand, Actor target)
    {
        if (IsDead())
        {
            return;
        }

        attackCommand.Execute(target);
        _animator.SetTrigger(ActionAnimatorTrigger);
    }

    private void Die()
    {
        _animator.SetTrigger(DieAnimatorTrigger);
        if (OnActorDieDelegate != null)
        {
            OnActorDieDelegate(this);
        }
    }    
}