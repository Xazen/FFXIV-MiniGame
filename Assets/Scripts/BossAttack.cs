using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BossAttack
{
    [Header("Boss Attack")]
    [SerializeField]
    private float _hpTrigger;

    [SerializeField]
    private int _probabilityWeight;

    [SerializeField]
    private ActionCommand _attack;

    [Header("Target Probablity")]
    [SerializeField]
    private int _tankProbability;

    [SerializeField]
    private int _knightProbability;

    [SerializeField]
    private int _blackMageProbability;

    [SerializeField]
    private int _whiteMageProbability;

    public void Execute()
    {
        Debug.Log(_attack.GetType().ToString());

        Actor target = GetTarget();
        if (target == null)
        {
            return;
        }
        _attack.Execute(target);
    }

    public float HpTrigger { get { return _hpTrigger; } }
    public int ProbabilityWeight { get { return _probabilityWeight; } }

    private Actor GetTarget()
    {
        GameSystem gameSystem = GameSystem.Instance();
        Actor target = null;
        do
        {
            // get alived actors
            List<Actor> alivedActors = new List<Actor>();
            int maxRandomValue = 0;
            if (!gameSystem.Tank.IsDead())
            {
                maxRandomValue += _tankProbability;
                alivedActors.Add(gameSystem.Tank);
            }

            if (!gameSystem.MeleeDps.IsDead())
            {
                maxRandomValue += _knightProbability;
                alivedActors.Add(gameSystem.MeleeDps);
            }

            if (!gameSystem.BlackMage.IsDead())
            {
                maxRandomValue += _blackMageProbability;
                alivedActors.Add(gameSystem.BlackMage);
            }

            if (!gameSystem.Player.IsDead())
            {
                maxRandomValue += _whiteMageProbability;
                alivedActors.Add(gameSystem.Player);
            }

            // no alived actors
            if (maxRandomValue == 0)
            {
                return null;
            }
            
            // calc random value
            int randomValue = UnityEngine.Random.Range(0, maxRandomValue);
            for (int i = 0; i < alivedActors.Count; i++)
            {
                Actor actor = alivedActors[i];

                // sum of probablities
                int attackProbability = 0;
                for (int j = 0; j < i + 1; j++)
                {
                    Actor actorProbability = alivedActors[j];
                    if (gameSystem.Tank == actorProbability)
                    {
                        attackProbability += _tankProbability;
                    }
                    else if (gameSystem.MeleeDps == actorProbability)
                    {
                        attackProbability += _knightProbability;
                    }
                    else if (gameSystem.BlackMage == actorProbability)
                    {
                        attackProbability += _blackMageProbability;
                    }
                    else if (gameSystem.Player == actorProbability)
                    {
                        attackProbability += _whiteMageProbability;
                    }
                }
                
                // random value < sum of probablities?
                if (randomValue < attackProbability)
                {
                    // found target
                    target = actor;
                    break;
                }
            }
        } while (target == null || target.IsDead());

        return target;        
    }
}