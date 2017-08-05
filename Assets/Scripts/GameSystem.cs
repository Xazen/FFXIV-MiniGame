using System;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Player Player;
    public PartyMember BlackMage;
    public PartyMember Tank;
    public PartyMember MeleeDps;
    public Boss Hydra;

    public static GameSystem Instance()
    {
        return GameObject.FindGameObjectWithTag("GameSystem").GetComponent<GameSystem>();
    }

    public void Start()
    {
        Player.OnHpChangedDelegate += OnPlayerHpChanged;
        Hydra.OnHpChangedDelegate += OnHydraHpChanged;
    }

    private void OnHydraHpChanged(Actor actor, int oldValue, int newValue)
    {
        if (newValue <= 0)
        {
            //TODO you win
        }
    }

    private void OnPlayerHpChanged(Actor actor, int oldValue, int newValue)
    {
        if (newValue <= 0)
        {
            //TODO you lose
        }
    }
}
