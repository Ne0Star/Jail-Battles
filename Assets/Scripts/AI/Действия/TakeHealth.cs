using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeHealth : AIAction
{
    [SerializeField] private Nurse executor;
    [SerializeField] private Entity target;
    public TakeHealth(Nurse executor, Entity target, UnityEvent complete)
    {
        this.executor = executor;
        this.target = target;
        complete.AddListener(Complete);
    }

    private void Complete()
    {
        if (target)
            target.HitBar.AddHealth(LevelManager.Instance.LevelData.HealValue);
        onComplete?.Invoke(this);
    }
    [SerializeField] private bool startHeal = false;
    public override void CustomUpdate()
    {
        if (!target || (target && !target.gameObject.activeSelf))
        {
            onComplete?.Invoke(this);
            return;
        }
        else
        {
            GameUtils.LookAt2DSmooth(executor.RotateParent, target.Agent.transform.position, executor.RotateOffset, Time.unscaledDeltaTime * executor.RotateSpeed);
        }
        float distance = Vector2.Distance(executor.Agent.transform.position, target.Agent.transform.position);
        if (distance <= executor.Agent.radius + target.Agent.radius + 0.1f)
        {
            if (!startHeal)
            {
                executor.Animator.Play("health");
                startHeal = true;
            }

        }
    }

    public override void Initial()
    {
        executor.Agent.isStopped = true;
        executor.Agent.SetDestination(target.transform.position);
        executor.Animator.Play("walk");
    }
}
