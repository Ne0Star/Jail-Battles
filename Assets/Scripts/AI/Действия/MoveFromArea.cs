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

    //[SerializeField] private float distance;
    [SerializeField] private Vector3 targetPos, lastPos;
    [SerializeField] private bool init = false;

    public override void CustomUpdate()
    {
        if (lastPos == targetPos)
        {
            executor.Agent.SetDestination(targetPos);
        }
        lastPos = targetPos;
        float distance = Vector2.Distance(executor.Agent.transform.position, targetPos);
        if (executor as Enemu)
        {
            Enemu e = (Enemu)executor;
            GameUtils.LookAt2DSmooth(e.RotateParent, e.Agent.transform.position + e.Agent.velocity, e.RotateOffset, Time.unscaledDeltaTime * (executor.Agent.speed * LevelManager.Instance.LevelData.RotateMultipler));
        }
        if (distance <= executor.Agent.radius * 2)
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
                executor.Animator.Play("walk", executor.Agent.speed);
            }
            else
            {
                onComplete?.Invoke(this);
            }

        });
    }
}
