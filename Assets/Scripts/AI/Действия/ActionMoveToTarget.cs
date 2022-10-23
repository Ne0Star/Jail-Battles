using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

/// <summary>
/// Двигаться к цели
/// </summary>

[System.Serializable]
public class ActionMoveToTarget : AIAction
{


    [SerializeField] private AI executor;
    [SerializeField] private Entity target;

    public ActionMoveToTarget(AI executor, Entity target)
    {
        this.executor = executor;
        this.target = target;
    }

    [SerializeField] private float breakDistance;
    [SerializeField] private bool breakToFar;
    public ActionMoveToTarget(AI executor, Entity target, float breakDistance)
    {
        this.executor = executor;
        this.target = target;
        this.breakToFar = true;
        this.breakDistance = breakDistance;
    }

    public override void CustomUpdate()
    {
        executor.Agent.SetDestination(target.transform.position);
        GameUtils.LookAt2D(executor.Data.RotateParent, executor.Agent.transform.position + executor.Agent.velocity, executor.Data.RotateOffset);
        float distance = distance = Vector2.Distance(executor.Agent.transform.position, target.transform.position);
        if (distance <= (executor.Agent.radius + target.Agent.radius) || (breakToFar && distance > breakDistance))
        {
            onComplete?.Invoke();
        }
    }

    public override void Initial()
    {

    }

    public override void Break()
    {

    }
}
