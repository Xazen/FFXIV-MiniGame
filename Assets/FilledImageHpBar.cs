using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FilledImageHpBar : MonoBehaviour
{
    [SerializeField]
    private Actor _actor;

    private Image _hpBarImage;
	
	void Start ()
    {
        _hpBarImage = GetComponent<Image>();

        _hpBarImage.fillAmount = _actor.MaxHP;
        _actor.OnHpChangedDelegate += OnHpChanged;
	}

    private void OnHpChanged(Actor actor, int oldValue, int newValue)
    {
        _hpBarImage.fillAmount = (float) newValue / actor.MaxHP;
    }

    private void OnDestroy()
    {
        _actor.OnHpChangedDelegate -= OnHpChanged;
    }
}