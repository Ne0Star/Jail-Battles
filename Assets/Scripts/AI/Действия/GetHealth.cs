using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealth : AIAction
{
    [SerializeField] private Entity executor;
    [SerializeField] private Nurse nurse;
    [SerializeField] private bool initialized = false;
    public GetHealth(Entity executor)
    {
        this.executor = executor;
    }
    public override void CustomUpdate()
    {
        if (initialized)
        {



            executor.Agent.SetDestination(nurse.Agent.transform.position);
            if (AIUtils.Collision(executor.Agent, nurse.Agent))
            {
                initialized = false;
                executor.Animator.Play("idle");
                nurse.GetHealth(executor, () =>
                {
                    OnComplete?.Invoke(this);
                });
            }
            else
            {
                if (executor as Enemu)
                {
                    Enemu e = (Enemu)executor;
                    GameUtils.LookAt2DSmooth(e.RotateParent, e.Agent.transform.position + e.Agent.velocity, e.RotateOffset, Time.unscaledDeltaTime * e.RotateSpeed);
                }
            }
        } else
        {
            if (executor as Enemu)
            {
                Enemu e = (Enemu)executor;
                GameUtils.LookAt2DSmooth(e.RotateParent, nurse.Agent.transform.position, e.RotateOffset, Time.unscaledDeltaTime * e.RotateSpeed);
            }
        }
    }

    public override void Initial()
    {
        this.nurse = LevelManager.Instance.EnemuManager.GetEntityByType<Nurse>();
        if (!nurse || (nurse && !nurse.IsFree))
        {
            OnComplete?.Invoke(this);
            return;
        }
        executor.Agent.isStopped = false;
        initialized = true;
        executor.Animator.Play("walk", executor.Agent.speed);
    }
}
