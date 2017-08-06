using UnityEngine;
using UnityEngine.UI;

public class CastGUI : MonoBehaviour
{
    [SerializeField]
    private Player _actor;

    [SerializeField]
    private Image _castBarImage;

    [SerializeField]
    private Text _castText;

    void Start()
    {
        gameObject.SetActive(false);
        _castBarImage.fillAmount = 0;
        _actor.OnCastTimeChangedDelegate += OnCastTimeChanged;
    }

    private void OnCastTimeChanged(Actor actor, float oldCastTime, float newCastTime, float maxCastTime)
    {
        _castBarImage.fillAmount = 1f - (newCastTime / maxCastTime);
        _castText.text = newCastTime.ToString();

        gameObject.SetActive(newCastTime > 0);
    }
}
