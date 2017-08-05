using UnityEngine;
using UnityEngine.UI;

public class MpGUI : MonoBehaviour
{

    [SerializeField]
    private Actor _actor;

    [SerializeField]
    private Image _mpBarImage;

    [SerializeField]
    private Text _mpText;

    void Start()
    {
        _mpBarImage.fillAmount = _actor.MaxMP;
        _mpText.text = _actor.MaxMP.ToString();
        _actor.OnMpChangedDelegate += OnMpChanged;
    }

    private void OnMpChanged(Actor actor, int oldValue, int newValue)
    {
        _mpBarImage.fillAmount = (float)newValue / actor.MaxMP;
        _mpText.text = newValue.ToString();
    }

    private void OnDestroy()
    {
        _actor.OnMpChangedDelegate -= OnMpChanged;
    }
}
