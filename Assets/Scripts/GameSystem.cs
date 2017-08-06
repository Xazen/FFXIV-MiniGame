using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource MusicSource;
    public AudioSource JingleSource;
    public AudioSource SoundSource;

    [Header("Clips")]
    public AudioClip OpenWindow;
    public AudioClip DutyStartJinggle;
    public AudioClip DutyFailedJinggle;
    public AudioClip DutyCompleteJinggle;
    public AudioClip FullPartyJinggle;
    public AudioClip BackgroundMusic;
    public AudioClip WinMusic;

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
        JingleSource.PlayOneShot(DutyStartJinggle);
        yield return new WaitForSeconds(4.5f);
        DutyStart.SetActive(false);
        JingleSource.PlayOneShot(FullPartyJinggle);
        GameRunning = true;
        MusicSource.loop = true;
        MusicSource.clip = BackgroundMusic;
        MusicSource.Play();
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
        MusicSource.Stop();
        JingleSource.PlayOneShot(DutyCompleteJinggle);
        yield return new WaitForSeconds(2.5f);
        DutyComplete.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        FinishDialog.SetActive(true);
        SoundSource.PlayOneShot(OpenWindow);
        MusicSource.clip = WinMusic;
        MusicSource.loop = false;
        MusicSource.Play();
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
        JingleSource.PlayOneShot(DutyFailedJinggle);
        yield return new WaitForSeconds(3.5f);
        DutyFailed.SetActive(false);
        SceneManager.LoadScene("Accept");
    }
}
