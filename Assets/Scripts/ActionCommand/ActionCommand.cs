using UnityEngine;

public abstract class ActionCommand : MonoBehaviour
{
    private Actor _target;
    public Actor Target { get { return _target; } }
    
    public virtual void Execute(Actor target)
    {
        _target = target;
    }
}