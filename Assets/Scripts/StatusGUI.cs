using UnityEngine;
using UnityEngine.UI;

public class StatusGUI : MonoBehaviour {

    [SerializeField]
    private Actor _actor;

    [SerializeField]
    private Image _protectImage;

    [SerializeField]
    private Image _paralysisImage;

    void Start()
    {
        _protectImage.gameObject.SetActive(_actor.IsProtectEnabled());
        _paralysisImage.gameObject.SetActive(_actor.IsParalyzed);
        _actor.OnProtectChangedDelegate += OnProtectChanged;
        _actor.OnParalysisChangedDelegate += OnParalysisChanged;
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
