using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromArea : AIAction
{
    [SerializeField] private Entity executor;
    [SerializeField] private List<AIArea> areas;
    public MoveFromArea(Entity executor, List<AIArea> areas)
    {
        this.executor = executor;
        this.areas = areas;
    }

    public override void Break()
    {

    }

    [SerializeField] private float distance;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private bool init = false;
    public override void CustomUpdate()
    {
        if (!init) return;
        float distance = Vector2.Distance(executor.Agent.transform.position, targetPos);
        if (executor as Enemu)
        {
            Enemu e = (Enemu)executor;
            GameUtils.LookAt2DSmooth(e.RotateParent, e.Agent.transform.position + e.Agent.velocity, e.RotateOffset, Time.unscaledDeltaTime * e.RotateSpeed);
        }
        if (distance <= executor.Agent.radius)
        {
            onComplete?.Invoke(this);
        }

    }

    public override void Initial()
    {
        executor.Agent.isStopped = false;
        areas[Random.Range(0, areas.Count)].GetVector(executor.Agent, (v) =>
        {
            if (executor.gameObject.activeSelf)
            {
                targetPos = v;
                executor.Agent.SetDestination(v);
                init = true;
                executor.Animator.Play("walk");
            }
            else
            {
                onComplete?.Invoke(this);
            }

        });
    }
}
