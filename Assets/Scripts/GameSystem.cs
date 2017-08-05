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
}
