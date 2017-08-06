using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class ActionCommand : MonoBehaviour
{
    public AudioClip Sfx;

    private Actor _target;
    public Actor Target { get { return _target; } }

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public virtual void Execute(Actor target)
    {
        if (Sfx != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(Sfx);
        }
        _target = target;
    }
}