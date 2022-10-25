using System.Collections;
using UnityEngine;

/// <summary>
/// ƒвигатьс€ к цели
/// </summary>

[System.Serializable]
public class ActionMoveToTarget : AIAction
{
    [SerializeField] private bool breaking = false;
    [SerializeField] private AI executor;
    [SerializeField] private Entity target;
    public ActionMoveToTarget(AI executor, Entity target)
    {
        this.executor = executor;
        this.target = target;
    }

    public void SetTarget(Entity target)
    {
        this.target = target;
    }

    [SerializeField] private float breakDistance;
    [SerializeField] private bool breakToFar;
    /// <summary>
    /// ѕриследовать цель до тех пор пока она не скроетс€ или не будет достигнута
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="target"></param>
    /// <param name="breakDistance"></param>
    public ActionMoveToTarget(AI executor, Entity target,float breakDistance)
    {
        this.executor = executor;
        this.target = target;
        this.breakDistance = breakDistance;
        this.breakToFar = true;
    }

    [SerializeField] private float distance;
    public override void CustomUpdate()
    {
        if (breaking) return;
        distance = Vector2.Distance(executor.Agent.transform.position, target.Agent.transform.position);

        if (distance <= (executor.Agent.radius + target.Agent.radius + 0.01f))
        {
            onComplete?.Invoke(this);
        }
        else if (breakToFar && distance >= breakDistance)
        {
            onBreak?.Invoke(this);
        }
        else
        {
            executor.Agent.SetDestination(target.Agent.transform.position);
            GameUtils.LookAt2D(executor.RotateParent, executor.Agent.transform.position + executor.Agent.velocity, executor.RotateOffset);
        }
    }

    public override void Initial()
    {
        breaking = false;
        executor.Stats.Walk.Play(executor.Animator, executor.Source, executor.Agent.speed);
        executor.Agent.SetDestination(target.Agent.transform.position);
    }

    public override void Break()
    {
        breaking = true;
    }
}
