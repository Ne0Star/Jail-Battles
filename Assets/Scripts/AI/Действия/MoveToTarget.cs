using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : AIAction
{
    public System.Action onExitDistance;

    [SerializeField] private Entity target;
    [SerializeField] private Entity executor;
    [SerializeField] private Transform transformTarget;
    [SerializeField] private float breakDistance;
    [SerializeField] private bool useBreakDistance = false;
    [SerializeField] private bool useTransform;
    public MoveToTarget(Entity executor, Entity target)
    {
        this.executor = executor;
        this.target = target;
        useBreakDistance = false;
    }
    public MoveToTarget(Entity executor, Transform target)
    {
        this.executor = executor;
        this.transformTarget = target;
        useBreakDistance = false;
        useTransform = true;
    }
    public MoveToTarget(Entity executor, Entity target, float breakDistance)
    {
        this.executor = executor;
        this.target = target;
        this.breakDistance = breakDistance;
        useBreakDistance = true;
    }

    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector2 targetPosition;
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
                GameUtils.LookAt2DSmooth(e.RotateParent, targetPosition, e.RotateOffset, Time.unscaledDeltaTime * e.RotateSpeed);
                //GameUtils.LookAt2D(e.RotateParent, target.position, e.RotateOffset);
            }
        }
    }
    public override void CustomUpdate()
    {
        targetPosition = useTransform ? transformTarget.position : target.Agent.transform.position;
        float distance = Vector2.Distance(executor.Agent.transform.position, targetPosition);
        Rotate();

        if (distance <= (useTransform ? executor.Agent.radius : executor.Agent.radius + target.Agent.radius + 0.1f))
        {
            onComplete?.Invoke(this);
        }
        else if (useBreakDistance && distance >= breakDistance)
        {
            onExitDistance?.Invoke();
        }
        executor.Agent.SetDestination(targetPosition);
    }

    public override void Initial()
    {
        executor.Agent.isStopped = false;
        executor.Animator.Play("walk", executor.Agent.speed);
    }
}
