using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    public AudioSource TargetSource;
    public AudioClip SwitchTarget;

    [SerializeField]
    private GameObject _playerArrow;

    [SerializeField]
    private GameObject _blackMageArrow;

    [SerializeField]
    private GameObject _knightArrow;

    [SerializeField]
    private GameObject _tankArrow;

    [SerializeField]
    private Actor[] _selectOrder;

    private GameSystem _gameSystem;

    public delegate void TargetChangedDelegate(Actor newTarget);
    public TargetChangedDelegate OnTargetChangedDelegate;
      
    private void Start()
    {
        _gameSystem = GameSystem.Instance();
        SelectTarget(null);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {

                if (hit.transform.gameObject.tag.Equals("Actor"))
                {
                    SelectTarget(hit.transform.gameObject.GetComponent<Actor>());
                }
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Actor currentTarget = _gameSystem.Player.Target;
            if (currentTarget == null)
            {
                SelectTarget(_selectOrder[0]);
            }

            for (int i = 0; i < _selectOrder.Length; i++)
            {
                Actor actor = _selectOrder[i];
                if (actor == currentTarget)
                {
                    if (i + 1 < _selectOrder.Length)
                    {
                        SelectTarget(_selectOrder[i + 1]);
                    }
                    else
                    {
                        SelectTarget(_selectOrder[0]);
                    }
                }
            }
        }
    }

    public void SelectTarget(Actor actor)
    {
        TargetSource.PlayOneShot(SwitchTarget);
        _playerArrow.SetActive(actor == _gameSystem.Player);
        _blackMageArrow.SetActive(actor == _gameSystem.BlackMage);
        _knightArrow.SetActive(actor == _gameSystem.MeleeDps);
        _tankArrow.SetActive(actor == _gameSystem.Tank);

        _gameSystem.Player.SelectTarget(actor);
        if (OnTargetChangedDelegate != null)
        {
            OnTargetChangedDelegate(actor);
        }        
    }
}
