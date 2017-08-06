using UnityEngine;
using UnityEngine.UI;

public class StatusGUI : MonoBehaviour {

    [SerializeField]
    private Actor _actor;
    
    [SerializeField]
    private Image _protectImage;

    [SerializeField]
    private Image _paralysisImage;

    [SerializeField]
    private Button _targetSelectButton;

    [SerializeField]
    private GameObject _targetActiveObject;

    void Start()
    {
        _targetActiveObject.SetActive(false);
        _targetSelectButton.onClick.AddListener(OnTargetSelectClick);

        _protectImage.gameObject.SetActive(_actor.IsProtectEnabled());
        _paralysisImage.gameObject.SetActive(_actor.IsParalyzed);
        _actor.OnProtectChangedDelegate += OnProtectChanged;
        _actor.OnParalysisChangedDelegate += OnParalysisChanged;

        GameSystem.Instance().TargetSystem.OnTargetChangedDelegate += OnTargetChanged;
    }

    private void OnTargetSelectClick()
    {
        GameSystem.Instance().SelectTarget(_actor);
    }
    
    private void OnTargetChanged(Actor newTarget)
    {
        _targetActiveObject.SetActive(newTarget == _actor);
    }

    private void OnParalysisChanged(Actor actor, bool isActive)
    {
        _paralysisImage.gameObject.SetActive(isActive);
    }

    private void OnProtectChanged(Actor actor, bool isActive)
    {
        _protectImage.gameObject.SetActive(isActive);
    }
}
