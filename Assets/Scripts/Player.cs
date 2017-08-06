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
    private PartyMember _target;

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

    public void SelectTarget(PartyMember partyMember)
    {
        _target = partyMember;
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