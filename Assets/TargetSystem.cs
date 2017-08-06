using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerArrow;

    [SerializeField]
    private GameObject _blackMageArrow;

    [SerializeField]
    private GameObject _knightArrow;

    [SerializeField]
    private GameObject _tankArrow;

    private GameSystem _gameSystem;
      
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
    }

    public void SelectTarget(Actor actor)
    {
        _playerArrow.SetActive(actor == _gameSystem.Player);
        _blackMageArrow.SetActive(actor == _gameSystem.BlackMage);
        _knightArrow.SetActive(actor == _gameSystem.MeleeDps);
        _tankArrow.SetActive(actor == _gameSystem.Tank);

        _gameSystem.Player.SelectTarget(actor);
    }
}
