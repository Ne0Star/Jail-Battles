using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttack : AIAction
{
    [SerializeField] private AI executor;
    [SerializeField] private Entity target;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    public ActionAttack(AI executor, Entity target, float attackSpeed, float attackDamage)
    {
        this.executor = executor;
        this.target = target;
        this.attackSpeed = attackSpeed;
        this.attackDamage = attackDamage;
    }

    public override void Break()
    {
        executor = null;
        isAttack = false;
        currentFight = null;
        currentTime = 0;
        target = null;
    }
    [SerializeField]
    private float currentTime;
    [SerializeField]
    private AIFightStat currentFight;
    [SerializeField]
    private bool isAttack = false;

    public override void CustomUpdate()
    {
        if (!target || !target.gameObject.activeInHierarchy)
        {
            onComplete?.Invoke(this);
            return;
        }
        if (Vector2.Distance(executor.Agent.transform.position, target.Agent.transform.position) <= (executor.Agent.radius + target.Agent.radius + 0.01f))
        {
            GameUtils.LookAt2D(executor.RotateParent, target.Agent.transform.position, executor.RotateOffset);

            if (!isAttack)
            {
                executor.Agent.SetDestination(executor.Agent.transform.position);
                executor.Stats.FigtStance.Play(executor.Animator, executor.Source, attackSpeed);
                isAttack = true;
            }

            if (currentFight == null)
            {
                currentFight = executor.Stats.GetStandartStrike();
                //currentTime = currentFight.attackSpeed;
            }
            else
            {

                if (currentTime >= (currentFight.attackSpeed * attackSpeed))
                {

                    currentFight.presset.Play(executor.Animator, executor.Source);
                    target.TakeDamage(executor.Entity, currentFight.damage * attackDamage);

                    currentFight = null;
                    currentTime = 0f;
                }
                else
                {
                    currentTime += 0.02f;
                }
            }
        }
        else
        {
            onBreak?.Invoke(this);
        }
    }

    public override void Initial()
    {
    }
}
