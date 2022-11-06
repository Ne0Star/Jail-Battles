using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHealth : AIAction
{
    [SerializeField] private Nurse executor;
    [SerializeField] private Entity target;
    public TakeHealth(Nurse executor, Entity target, System.Action complete)
    {
        this.executor = executor;
        this.target = target;
        complete += Complete;
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
        if (AIUtils.Collision(executor.Agent, target.Agent))
        {
            GameUtils.LookAt2DSmooth(executor.RotateParent, target.Agent.transform.position, executor.RotateOffset, Time.unscaledDeltaTime * executor.RotateSpeed);
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
        executor.Animator.Play("idle");
    }
}
