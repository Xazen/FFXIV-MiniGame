using UnityEngine;

public abstract class ActionCommand : MonoBehaviour
{
    private Actor _target;
    
    public virtual void Execute(Actor target)
    {
        _target = target;
    }
}