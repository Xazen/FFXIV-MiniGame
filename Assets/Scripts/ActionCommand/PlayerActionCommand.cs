using UnityEngine;

public abstract class PlayerActionCommand : ActionCommand
{
    private float _castTime;
    private float _recastTime;

    private float _currentRecastTime;

    public delegate void CastTimeChangedDelegate(PlayerActionCommand action, float oldRecastTime, float newRecastTime);
    public event CastTimeChangedDelegate OnCastTimeChangedDelegate;

    public delegate void PlayerActionReadyDelegate(PlayerActionCommand action);
    public event PlayerActionReadyDelegate OnPlayerActionReadyDelegate;

    public delegate void PlayerActionStarted(PlayerActionCommand action);
    public event PlayerActionStarted OnPlayerActionStarted;

    public override void Execute(Actor target)
    {
        base.Execute(target);
        if (_currentRecastTime > 0)
        {
            return;
        }
        _currentRecastTime = _recastTime;

        if (OnPlayerActionStarted != null)
        {
            OnPlayerActionStarted(this);
        }
    }

    public void Update()
    {
        float oldRecastTime = _currentRecastTime;
        _currentRecastTime = Mathf.Max(_currentRecastTime - Time.deltaTime, 0);

        if (OnCastTimeChangedDelegate != null &&
            oldRecastTime != _currentRecastTime)
        {
            OnCastTimeChangedDelegate(this, oldRecastTime, _currentRecastTime);
        }

        if (oldRecastTime > 0 &&
            _currentRecastTime <= 0 &&
            OnPlayerActionReadyDelegate != null)
        {
            OnPlayerActionReadyDelegate(this);
        }
    }
}