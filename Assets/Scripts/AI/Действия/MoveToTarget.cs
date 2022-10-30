using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : AIAction
{

    [SerializeField] private Entity target;
    [SerializeField] private Entity executor;
    [SerializeField] private float breakDistance;
    public MoveToTarget(Entity executor, Entity target, float breakDistance)
    {
        this.executor = executor;
        this.target = target;
        this.breakDistance = breakDistance;
    }

    public override void Break()
    {

    }

    [SerializeField] private Vector3 targetPos;

    public override void CustomUpdate()
    {
        float distance = Vector2.Distance(executor.Agent.transform.position, target.Agent.transform.position);
        if (executor as Enemu)
        {
            Enemu e = (Enemu)executor;
            GameUtils.LookAt2D(e.RotateParent, e.Agent.transform.position + e.Agent.velocity, e.RotateOffset);
        }
        if (distance <= executor.Agent.radius + target.Agent.radius)
        {
            onComplete?.Invoke(this);
        }
        else if (distance >= breakDistance)
        {
            OnBreak?.Invoke(this);
        }
        executor.Agent.SetDestination(target.transform.position);
    }

    public override void Initial()
    {
        executor.Animator.Play("walk");
    }
}
