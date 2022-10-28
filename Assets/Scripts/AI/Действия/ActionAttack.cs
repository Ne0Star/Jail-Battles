using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAttack : AIAction
{
    private AI executor;
    [SerializeField] private Entity target;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private Weapon weapon;
    public ActionAttack(AI executor, Entity target, float attackSpeed, float attackDamage)
    {
        this.executor = executor;
        this.target = target;
        this.attackSpeed = attackSpeed;
        this.attackDamage = attackDamage;
        this.weapon = executor.Weapon;
    }

    public override void Break()
    {
        weapon = null;
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
        if (!weapon)
        {
            if (AIUtils.Collision(executor.Agent, target.Agent))
            {
                executor.Stats.FigtStance.Play(executor.Animator, executor.Source, attackSpeed, executor.Weapon);
                if(!target.gameObject.activeSelf)
                {
                    onComplete?.Invoke(this);
                }
                if (currentTime >= weapon.ReloadSpeed)
                {

                    executor.Stats.Attack(executor.Animator, executor.Source, attackSpeed);

                    currentTime = 0f;
                }
                currentTime += 0.02f;
            } else
            {
                onBreak?.Invoke(this);
            }
        }
        else
        {
            if (weapon.WeaponType == WeaponType.ќдноручное)
            {
                //if (AIUtils.Collision(executor.Agent, target.Agent))
                //{
                //    // в зависимости от активного типа оружи€, запустить нужную анимацию боевой стойки

                //    executor.Stats.FigtStance.Play(executor.Animator, executor.Source, attackSpeed, executor.Weapon);

                //    if (currentTime >= weapon.ReloadSpeed)
                //    {

                //        executor.Stats.Attack(executor.Animator, executor.Source, attackSpeed);

                //        currentTime = 0f;
                //    }
                //    currentTime += 0.02f;
                //}
            }
            else
            {

            }
        }
    }


    //public override void CustomUpdate()
    //{

    //    if (!target || !target.gameObject.activeInHierarchy)
    //    {
    //        onComplete?.Invoke(this);
    //        return;
    //    }
    //    if (Vector2.Distance(executor.Agent.transform.position, target.Agent.transform.position) <= (executor.Agent.radius + target.Agent.radius + 0.01f))
    //    {
    //        GameUtils.LookAt2D(executor.RotateParent, target.Agent.transform.position, executor.RotateOffset);

    //        if (!isAttack)
    //        {
    //            executor.Agent.SetDestination(executor.Agent.transform.position);
    //            executor.Stats.FigtStance.Play(executor.Animator, executor.Source, attackSpeed, executor.Weapon);
    //            isAttack = true;
    //        }

    //        if (currentFight == null)
    //        {
    //            currentFight = executor.Stats.GetStandartStrike();
    //            //currentTime = currentFight.attackSpeed;
    //        }
    //        else
    //        {

    //            if (currentTime >= (currentFight.attackSpeed * attackSpeed))
    //            {

    //                currentFight.presset.Play(executor.Animator, executor.Source);
    //                target.TakeDamage(executor.Entity, currentFight.damage * attackDamage, () =>
    //                {
    //                    executor.UpdateCurrentAI();
    //                });

    //                currentFight = null;
    //                currentTime = 0f;
    //            }
    //            else
    //            {
    //                currentTime += 0.02f;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        onBreak?.Invoke(this);
    //    }
    //}

    public override void Initial()
    {
        currentTime = float.MaxValue;
    }
}
