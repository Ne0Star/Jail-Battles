using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : AIAction
{
    [SerializeField] private Entity executor;
    [SerializeField] private Entity target;
    [SerializeField] private Weapon weapon;

    [SerializeField] private float exitDistance;

    public AttackTarget(Entity executor, Entity target, Weapon weapon, float exitDistance)
    {
        this.executor = executor;
        this.target = target;
        this.weapon = weapon;
        this.exitDistance = exitDistance;
    }

    [SerializeField] private float currentTime;

    public override void Break()
    {

    }

    private void Rotate()
    {
        if (executor as Enemu)
        {
            Enemu e = (Enemu)executor;
            if (!reached)
            {
                GameUtils.LookAt2D(e.RotateParent, e.Agent.transform.position + e.Agent.velocity, e.RotateOffset);
            }
            else
            {
                GameUtils.LookAt2D(e.RotateParent, target.Agent.transform.position, e.RotateOffset);
            }
        }
    }
    [SerializeField] private bool reached = false;
    public override void CustomUpdate()
    {
        float distance = Vector2.Distance(executor.Agent.transform.position, target.Agent.transform.position);
        Rotate();
        if (target && target.gameObject.activeSelf)
        {
            if (distance <= weapon.AttackDistance)
            {
                if(!reached)
                {
                    executor.Animator.Play("idle");
                }
                reached = true;

                executor.Agent.isStopped = true;
                if (currentTime >= weapon.ReloadSpeed)
                {
                    target.TakeDamage(executor, weapon.AttackDamage, () =>
                    {

                    });
                    currentTime = 0;
                }
                currentTime += Time.unscaledDeltaTime;
            }
            else
            {
                executor.Agent.isStopped = false;
                executor.Agent.SetDestination(target.Agent.transform.position);
            }
            if (distance >= exitDistance)
            {
                OnBreak?.Invoke(this);
            }
        }
        else
        {
            onComplete?.Invoke(this);
        }
    }

    public override void Initial()
    {
        currentTime = float.MaxValue;
    }
}
