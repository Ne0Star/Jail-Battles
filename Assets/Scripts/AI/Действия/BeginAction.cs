using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginAction : AIAction
{
    public System.Action onDied;

    [SerializeField] private Enemu executor;
    [SerializeField] private Enemu pursuer;
    [SerializeField] private List<AIArea> areas;
    [SerializeField] private float speedInterpolator;
    public BeginAction(Enemu executor, Enemu pursuer, List<AIArea> areas, float speedInterpolator)
    {
        this.executor = executor;
        this.pursuer = pursuer;
        this.areas = areas;
        this.speedInterpolator = speedInterpolator;
    }

    public override void CustomUpdate()
    {
        float distance = Vector2.Distance(executor.Agent.transform.position, pursuer.Agent.transform.position);

        if (executor as Enemu)
        {
            Enemu e = (Enemu)executor;
            e.Agent.speed = Mathf.Clamp(e.Agent.speed + speedInterpolator, -e.MoveSpeed.MaxValue, e.MoveSpeed.MaxValue);
            GameUtils.LookAt2DSmooth(e.RotateParent, e.Agent.transform.position + e.Agent.velocity, e.RotateOffset, Time.unscaledDeltaTime * e.RotateSpeed);
        }
        if (!pursuer || !pursuer.gameObject.activeSelf || !(pursuer.Target == executor))
        {
            onComplete?.Invoke(this);
        }
        if (!executor.gameObject.activeSelf)
        {
            onDied?.Invoke();
        }
    }
    [SerializeField] private Vector3 targetPos;
    public override void Initial()
    {
        executor.Agent.isStopped = false;
        areas[Random.Range(0, areas.Count)].GetVector(executor.Agent, (v) =>
        {
            if (executor.gameObject.activeSelf)
            {
                targetPos = v;
                executor.Agent.SetDestination(v);
                executor.Animator.Play("walk", executor.Agent.speed);
            }
            else
            {
                onComplete?.Invoke(this);
            }

        });
    }
}
