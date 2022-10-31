using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : AIAction
{
    [SerializeField] private Enemu executor;
    [SerializeField] private Entity target;
    [SerializeField] private float exitDistance;

    public AttackTarget(Enemu executor, Entity target, float exitDistance, bool fastAttack)
    {
        this.executor = executor;
        this.target = target;
        this.exitDistance = exitDistance;
        if (fastAttack)
        {
            currentTime = float.MaxValue;
        }
        else { currentTime = 0f; }
    }

    [SerializeField] private float currentTime;

    public override void Break()
    {

    }

    private void Rotate()
    {
            if (!reached)
            {
                GameUtils.LookAt2D(executor.RotateParent, executor.Agent.transform.position + executor.Agent.velocity, executor.RotateOffset);
            }
            else
            {
                GameUtils.LookAt2D(executor.RotateParent, target.Agent.transform.position, executor.RotateOffset);
            }
    }
    [SerializeField] private bool reached = false;
    public override void CustomUpdate()
    {
        float distance = Vector2.Distance(executor.Agent.transform.position, target.Agent.transform.position);
        float weaponDistance = Vector2.Distance(executor.Weapon.transform.position, target.Agent.transform.position);
        Rotate();
        if (target && target.gameObject.activeSelf)
        {
            if (weaponDistance <= executor.Weapon.AttackDistance)
            {
                if (!reached)
                {
                    executor.Animator.Play("idle");
                }
                reached = true;

                executor.Agent.isStopped = true;
                if (currentTime >= executor.Weapon.ReloadSpeed)
                {
                    executor.Weapon.AnimateAttack();
                    target.TakeDamage(executor, executor.Weapon.AttackDamage, () =>
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

    }
}
