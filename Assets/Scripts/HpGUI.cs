using UnityEngine;
using UnityEngine.UI;

public class HpGUI : MonoBehaviour {

    [SerializeField]
    private Actor _actor;

    [SerializeField]
    private Image _hpBarImage;

    [SerializeField]
    private Text _hpText;

    void Start()
    {
        _hpBarImage.fillAmount = _actor.MaxHP;
        _actor.OnHpChangedDelegate += OnHpChanged;

        _hpText.text = _actor.MaxHP.ToString();
        _actor.OnHpChangedDelegate += OnHpChanged;

    }

    private void OnHpChanged(Actor actor, int oldValue, int newValue)
    {
        _hpBarImage.fillAmount = (float)newValue / actor.MaxHP;
        _hpText.text = newValue.ToString();
    }

    private void OnDestroy()
    {
        _actor.OnHpChangedDelegate -= OnHpChanged;
        _actor.OnHpChangedDelegate -= OnHpChanged;
    }
}
