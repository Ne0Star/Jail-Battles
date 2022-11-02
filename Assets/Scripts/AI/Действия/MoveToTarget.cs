using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : AIAction
{

    [SerializeField] private Transform target;
    [SerializeField] private Entity executor;
    [SerializeField] private float breakDistance;
    [SerializeField] private bool useBreakDistance = false;

    public MoveToTarget(Entity executor, Transform target)
    {
        this.executor = executor;
        this.target = target;
        useBreakDistance = false;
    }

    public MoveToTarget(Entity executor, Transform target, float breakDistance)
    {
        this.executor = executor;
        this.target = target;
        this.breakDistance = breakDistance;
        useBreakDistance = true;
    }

    public override void Break()
    {

    }

    [SerializeField] private Vector3 targetPos;
    [SerializeField] private bool reached = false;
    private void Rotate()
    {
        if (executor as Enemu)
        {
            Enemu e = (Enemu)executor;
            if (!reached)
            {
                GameUtils.LookAt2DSmooth(e.RotateParent, e.Agent.transform.position + e.Agent.velocity, e.RotateOffset, Time.unscaledDeltaTime * e.RotateSpeed);
                //GameUtils.LookAt2D(e.RotateParent, e.Agent.transform.position + e.Agent.velocity, e.RotateOffset);
            }
            else
            {
                GameUtils.LookAt2DSmooth(e.RotateParent, target.position, e.RotateOffset, Time.unscaledDeltaTime * e.RotateSpeed);
                //GameUtils.LookAt2D(e.RotateParent, target.position, e.RotateOffset);
            }
        }
    }
    public override void CustomUpdate()
    {
        float distance = Vector2.Distance(executor.Agent.transform.position, target.position);
        Rotate();
        if (distance <= executor.Agent.radius)
        {
            onComplete?.Invoke(this);
        }
        else if (useBreakDistance && distance >= breakDistance)
        {
            OnBreak?.Invoke(this);
        }
        executor.Agent.SetDestination(target.transform.position);
    }

    public override void Initial()
    {
        executor.Agent.isStopped = false;
        executor.Animator.Play("walk");
    }
}
