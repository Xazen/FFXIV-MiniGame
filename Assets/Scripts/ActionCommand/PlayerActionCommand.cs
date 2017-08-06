using UnityEngine;

public abstract class PlayerActionCommand : ActionCommand
{
    [SerializeField]
    private int _mpCost;

    [SerializeField]
    private float _castTime;

    [SerializeField]
    private float _recastTime;

    public int MpCost { get { return _mpCost; } }
    public float CastTime { get { return _castTime; } }
    public float RecastTime { get { return _recastTime; } }
    public float CurrentRecastTime { get { return _currentRecastTime; } }

    private float _currentRecastTime;

    public delegate void CastTimeChangedDelegate(PlayerActionCommand action, float oldRecastTime, float newRecastTime);
    public event CastTimeChangedDelegate OnRecastTimeChangedDelegate;

    public delegate void PlayerActionReadyDelegate(PlayerActionCommand action);
    public event PlayerActionReadyDelegate OnPlayerActionReadyDelegate;

    public delegate void PlayerActionStarted(PlayerActionCommand action);
    public event PlayerActionStarted OnPlayerActionStarted;

    public override void Execute(Actor target)
    {
        base.Execute(target);
        if (OnPlayerActionStarted != null)
        {
            OnPlayerActionStarted(this);
        }
    }

    public void StartRecastTime()
    {
        if (_currentRecastTime > 0)
        {
            return;
        }
        _currentRecastTime = _recastTime;
    }

    public void Update()
    {
        float oldRecastTime = _currentRecastTime;
        _currentRecastTime = Mathf.Max(_currentRecastTime - Time.deltaTime, 0);

        if (OnRecastTimeChangedDelegate != null &&
            oldRecastTime != _currentRecastTime)
        {
            OnRecastTimeChangedDelegate(this, oldRecastTime, _currentRecastTime);
        }

        if (oldRecastTime > 0 &&
            _currentRecastTime <= 0 &&
            OnPlayerActionReadyDelegate != null)
        {
            OnPlayerActionReadyDelegate(this);
        }
    }
}