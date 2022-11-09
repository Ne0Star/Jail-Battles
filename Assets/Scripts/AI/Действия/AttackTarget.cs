using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : AIAction
{
    [SerializeField] private Enemu executor;
    [SerializeField] private Entity target;
    [SerializeField] private float exitDistance;

    public AttackTarget(Enemu executor, ref Entity target, bool fastAttack)
    {
        this.executor = executor;
        this.target = target;
        this.exitDistance = LevelManager.Instance.LevelData.ExitDistance;
        attackTime = 0;
        attackCount = 0;
        if (fastAttack)
        {
            currentTime = float.MaxValue;
        }
        else
        {

            currentTime = 0f;
        }
    }

    [SerializeField] private float currentTime;
    [SerializeField] private float attackTime;
    [SerializeField] private int attackCount;
    [SerializeField] private bool rotated = false;
    private void Rotate()
    {
        rotated = false;
        if (executor && executor.gameObject.activeSelf && target && target.gameObject.activeSelf)
        {
            if (!reached)
            {
                GameUtils.LookAt2DSmooth(executor.RotateParent, executor.Agent.transform.position + executor.Agent.velocity, executor.RotateOffset, Time.unscaledDeltaTime * (executor.Agent.speed * LevelManager.Instance.LevelData.RotateMultipler));
                    rotated = true;
                //rotated = true
                //GameUtils.LookAt2D(executor.RotateParent, executor.Agent.transform.position + executor.Agent.velocity, executor.RotateOffset);
            }
            else
            {
                GameUtils.LookAt2DSmooth(executor.RotateParent, target.Agent.transform.position, executor.RotateOffset, Time.unscaledDeltaTime * (executor.Agent.speed * LevelManager.Instance.LevelData.RotateMultipler), 0.1f, () =>
                {
                    rotated = true;
                });
                //GameUtils.LookAt2D(executor.RotateParent, target.Agent.transform.position, executor.RotateOffset);
            }
        }
        else
        {
            OnComplete?.Invoke(this);
        }

    }


    private void SimulateWeapon(Weapon w)
    {
        if (!reached)
        {
            attackCount = 0;
            attackTime = 0f;
            currentTime = 0f;
            executor.SetWeapon(w);
            executor.Animator.Play("fightStance");
        }
        reached = true;
        executor.Agent.isStopped = true;
        if (attackCount < executor.Weapon.AttackCount)
        {
            if (attackTime >= executor.Weapon.AttackSpeed)
            {

                executor.Animator.Play("attack");

                attackCount++;
                attackTime = 0;
            }
            attackTime += Time.unscaledDeltaTime;
        }
        else
        {
            if (currentTime >= executor.Weapon.ReloadSpeed)
            {

                //executor.Animator.Play("reload");

                attackCount = 0;
                currentTime = 0;
            }
            currentTime += Time.unscaledDeltaTime;
        }
    }
    public System.Action onExitDistance;
    [SerializeField] private bool reached = false;
    public override void CustomUpdate()
    {
        float distance = Vector2.Distance(executor.Agent.transform.position, target.Agent.transform.position);
        if (!target || !target.gameObject.activeSelf)
        {
            OnComplete?.Invoke(this);
            target = null;
            return;
        }
        if (distance >= exitDistance)
        {
            onExitDistance?.Invoke();
            onComplete?.Invoke(this);
            return;
        }


        // Есть пушка
        if (executor.WeaponGun && distance <= executor.WeaponGun.AttackDistance && distance >= executor.WeaponGun.NotAttackDistance && rotated)
        {
            SimulateWeapon(executor.WeaponGun);
        }
        // Есть дубинка
        else if (executor.WeaponMele && distance <= executor.Agent.radius + target.Agent.radius + 0.2f && rotated)
        {
            SimulateWeapon(executor.WeaponMele);
        }
        else
        {
            if (reached)
            {
                executor.Animator.Play("walk", executor.Agent.speed);
            }
            reached = false;
            executor.Agent.isStopped = false;
            //Debug.Log("Хуйня");
            executor.Agent.SetDestination(target.Agent.transform.position);
        }
        Rotate();
    }

    public override void Initial()
    {

    }
}
