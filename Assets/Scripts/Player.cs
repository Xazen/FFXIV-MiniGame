using UnityEngine;

public class Player : Actor
{
    public enum PlayerAction
    {
        Vita = 0,
        Vitra,
        Vitga,
        Esuna,
        Raise,
        Protect
    }

    [SerializeField]
    private PlayerActionCommand _vita;

    [SerializeField]
    private PlayerActionCommand _vitra;

    [SerializeField]
    private PlayerActionCommand _vitga;

    [SerializeField]
    private PlayerActionCommand _esuna;

    [SerializeField]
    private PlayerActionCommand _raise;

    [SerializeField]
    private PlayerActionCommand _protect;

    private PlayerActionCommand[] _playerActions = new PlayerActionCommand[6];
    private Actor _target;
    public Actor Target { get { return _target; } }

    public PlayerActionCommand _currentActionCommand;
    public PlayerActionCommand _pendingActionCommand;
    public float _currentCastTime;

    public delegate void CastTimeChangedDelegate(Actor actor, float oldCastTime, float newCastTime, float maxCastTime);
    public CastTimeChangedDelegate OnCastTimeChangedDelegate;

    public override void Start()
    {
        base.Start();
        _playerActions[(int)PlayerAction.Vita] = _vita;
        _playerActions[(int)PlayerAction.Vitra] = _vitra;
        _playerActions[(int)PlayerAction.Vitga] = _vitga;
        _playerActions[(int)PlayerAction.Esuna] = _esuna;
        _playerActions[(int)PlayerAction.Raise] = _raise;
        _playerActions[(int)PlayerAction.Protect] = _protect;
    }

    public void ExecuteCommand(PlayerActionCommand actionCommand)
    {
        if (actionCommand.CurrentRecastTime > 0 ||
            CurrentMP < actionCommand.MpCost ||
            _target == null ||
            !actionCommand.CanBeUsed(_target))
        {
            return;
        }

        if (_currentActionCommand == null)
        {
            _currentActionCommand = actionCommand;
            _currentCastTime = _currentActionCommand.CastTime;
            _currentActionCommand.StartRecastTime();
            _currentActionCommand.ForceTarget(_target);
        }
        else if (_currentCastTime <= 0.2f)
        {
            _pendingActionCommand = actionCommand;
        }
    }

    public void Update()
    {
        if (_currentCastTime > 0)
        {
            float oldCastTime = _currentCastTime;
            _currentCastTime = Mathf.Max(0, _currentCastTime - Time.deltaTime);

            if (_currentCastTime != oldCastTime &&
                OnCastTimeChangedDelegate != null)
            {
                OnCastTimeChangedDelegate(this, oldCastTime, _currentCastTime, _currentActionCommand.CastTime);
            }
        }

        if (_currentCastTime <= 0)
        {
            ReduceMp(_currentActionCommand.MpCost);
            _currentActionCommand.Execute(_target);
            _currentActionCommand = null;
        }

        if (_pendingActionCommand != null)
        {
            ExecuteCommand(_pendingActionCommand);
            _pendingActionCommand = null;
        }
    }

    public void SelectTarget(Actor actor)
    {
        _target = actor;
    }

    public void Vita()
    {
        _vita.Execute(_target);
    }

    public void Vitra()
    {
        _vitra.Execute(_target);
    }

    public void Vitga()
    {
        _vitga.Execute(_target);
    }

    public void Esuna()
    {
        _esuna.Execute(_target);
    }

    public void Raise()
    {
        _raise.Execute(_target);
    }

    public void Protect()
    {
        _protect.Execute(_target);
    }
}