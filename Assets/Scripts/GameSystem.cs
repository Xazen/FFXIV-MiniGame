using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    [Header("UI")]
    public GameObject FinishDialog;
    public GameObject DutyStart;
    public GameObject DutyFailed;
    public GameObject DutyComplete;

    [Header("Actor")]
    public Player Player;
    public PartyMember BlackMage;
    public PartyMember Tank;
    public PartyMember MeleeDps;
    public Boss Hydra;

    [Header("System")]
    public TargetSystem TargetSystem;
    public bool GameRunning { get; private set; }

    public static GameSystem Instance()
    {
        return GameObject.FindGameObjectWithTag("GameSystem").GetComponent<GameSystem>();
    }

    public void Start()
    {
        Player.OnHpChangedDelegate += OnPlayerHpChanged;
        Hydra.OnHpChangedDelegate += OnHydraHpChanged;

        StartCoroutine(StartDuty());
    }

    private IEnumerator StartDuty()
    {
        DutyStart.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        DutyStart.SetActive(false);
        GameRunning = true;
    }

    public void SelectTarget(Actor actor)
    {
        TargetSystem.SelectTarget(actor);
    }

    private void OnHydraHpChanged(Actor actor, int oldValue, int newValue)
    {
        if (newValue <= 0)
        {
            StartCoroutine(CompleteDuty());
        }
    }

    private IEnumerator CompleteDuty()
    {
        GameRunning = false;
        DutyComplete.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        DutyComplete.SetActive(false);
        yield return new WaitForSeconds(1f);
        FinishDialog.SetActive(true);
    }

    private void OnPlayerHpChanged(Actor actor, int oldValue, int newValue)
    {
        if (newValue <= 0)
        {
            StartCoroutine(FailDuty());
        }
    }

    private IEnumerator FailDuty()
    {
        GameRunning = false;
        DutyFailed.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        DutyFailed.SetActive(false);
        SceneManager.LoadScene("Accept");
    }
}
