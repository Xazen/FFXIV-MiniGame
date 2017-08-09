using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    private readonly float ProtectDamageMultiplier = 0.8f;
    private readonly float ParalysisDuration = 10f;

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

    [SerializeField]
    private TextMeshPro _healText;

    [SerializeField]
    private TextMeshPro _damageText;

    private int _currentHp;
    private float _timeUntilNextAutoHpRegeneration;
    
    private int _currentMp;
    private float _timeUntilNextAutoMpRegeneration;

    private bool _isProtectEnabled;
    private bool _paralysisActive;
    private float _currentParalysisTime;

    public delegate void HpChangedDelegate(Actor actor, int oldValue, int newValue);
    public event HpChangedDelegate OnHpChangedDelegate;

    public delegate void MpChangedDelegate(Actor actor, int oldValue, int newValue);
    public event MpChangedDelegate OnMpChangedDelegate;

    public delegate void ActorDieDelegate(Actor actor);
    public event ActorDieDelegate OnActorDieDelegate;

    public delegate void ActorRaiseDelegate(Actor actor);
    public event ActorRaiseDelegate OnActorRaiseDelegate;
    
    public delegate void ProtectChangedDelegate(Actor actor, bool isActive);
    public event ProtectChangedDelegate OnProtectChangedDelegate;

    public delegate void ParalysisChangedDelegate(Actor actor, bool isActive);
    public event ParalysisChangedDelegate OnParalysisChangedDelegate;

    public int MaxHP { get { return _maxHp; } }
    public int CurrentHP { get { return _currentHp; } }
    public int CurrentMP { get { return _currentMp; } }
    public int MaxMP { get { return _maxMp; } }
    public bool IsParalyzed { get { return _paralysisActive; } }


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
        ReduceParalysis();
    }

    public void CauseParalysis()
    {
        if (!_paralysisActive)
        {
            _paralysisActive = true;
            if (OnParalysisChangedDelegate != null)
            {
                OnParalysisChangedDelegate(this, true);
                _currentParalysisTime = ParalysisDuration;
            }
        }
    }

    public void CastProtect()
    {
        if (!_isProtectEnabled)
        {
            _isProtectEnabled = true;
            if (OnProtectChangedDelegate != null)
            {
                OnProtectChangedDelegate(this, true);
            }
        }
    }

    public void ReduceHp(int value)
    {
        if (IsDead() || value <= 0)
        {
            return;
        }

        if (_isProtectEnabled)
        {
            value = Mathf.RoundToInt(value * ProtectDamageMultiplier);
        }

        int oldValue = _currentHp;
        _currentHp = Mathf.Max(_currentHp - value, 0);

        DOTween.Kill(_damageText.transform);
        _damageText.text = value.ToString();
        _damageText.gameObject.SetActive(true);
        _damageText.transform.localPosition = new Vector3(0, 1.0f, -1);
        _damageText.transform.DOLocalMoveY(1.4f, 1f).OnComplete(OnDamageAnimationComplete);
        
        if (OnHpChangedDelegate != null)
        {
            OnHpChangedDelegate(this, oldValue, _currentHp);
        }

        if (_currentHp <= 0)
        {
            Die();
        }
    }

    private void OnDamageAnimationComplete()
    {
        _damageText.gameObject.SetActive(false);
    }

    public void IncreaseHp(int value)
    {
        if (IsDead() || value <= 0)
        {
            return;
        }

        int oldValue = _currentHp;
        _currentHp = Mathf.Min(_currentHp + value, _maxHp);

        DOTween.Kill(_healText.transform);
        _healText.text = value.ToString();
        _healText.gameObject.SetActive(true);
        _healText.transform.localPosition = new Vector3(0, 1.1f, -1);
        _healText.transform.DOLocalMoveY(1.3f, 1f).OnComplete(OnHealTextAnimationComplete);
        

        if (OnHpChangedDelegate != null)
        {
            OnHpChangedDelegate(this, oldValue, _currentHp);
        }
    }

    private void OnHealTextAnimationComplete()
    {
        _healText.gameObject.SetActive(false);
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
            OnMpChangedDelegate(this, oldValue, _currentMp);
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

    public bool IsProtectEnabled()
    {
        return _isProtectEnabled;
    }

    public void DisableParalysis()
    {
        if (_paralysisActive)
        {
            _paralysisActive = false;
            if (OnParalysisChangedDelegate != null)
            {
                OnParalysisChangedDelegate(this, false);
                _currentParalysisTime = 0;
            }
        }
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
    
    public void ExectueAction(PlayerActionCommand attackCommand, Actor target)
    {
        if (IsDead())
        {
            return;
        }

        attackCommand.Execute(target);
        _animator.SetTrigger(ActionAnimatorTrigger);
    }


    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(ActionAnimatorTrigger);
    }

    private void TryRegenerateMp()
    {
        _timeUntilNextAutoMpRegeneration -= Time.deltaTime;
        if (_timeUntilNextAutoMpRegeneration <= 0)
        {
            IncreaseMp(_autoMpRegenerateValue);
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

    private void ReduceParalysis()
    {
        _currentParalysisTime = Mathf.Max(_currentParalysisTime - Time.deltaTime, 0);
        if (_currentParalysisTime <= 0)
        {
            DisableParalysis();
        }
    }

    private void Die()
    {
        _animator.SetTrigger(DieAnimatorTrigger);
        DisableParalysis();

        if (_isProtectEnabled)
        {
            _isProtectEnabled = false;
            if (OnProtectChangedDelegate != null)
            {
                OnProtectChangedDelegate(this, false);
            }
        }

        if (OnActorDieDelegate != null)
        {
            OnActorDieDelegate(this);
        }
    }    
}