using UnityEngine;
using UnityEngine.UI;

public class PlayerCommandButton : MonoBehaviour
{
    [SerializeField]
    private KeyCode _keyCode;

    [SerializeField]
    private PlayerActionCommand _actionCommand;

    [SerializeField]
    private Image _recastImageOverlay;

    [SerializeField]
    private Button _actionButton;

	// Use this for initialization
	void Start ()
    {
        _recastImageOverlay.fillAmount = 0;
        _actionCommand.OnRecastTimeChangedDelegate += OnCastTimeChanged;
        _actionButton.onClick.AddListener(OnButtonClicked);
    }

    public void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            GameSystem.Instance().Player.ExecuteCommand(_actionCommand);
        }
    }

    private void OnButtonClicked()
    {
        GameSystem.Instance().Player.ExecuteCommand(_actionCommand);
    }

    private void OnCastTimeChanged(PlayerActionCommand action, float oldRecastTime, float newRecastTime)
    {
        _recastImageOverlay.fillAmount = newRecastTime / action.RecastTime;
    }
}
