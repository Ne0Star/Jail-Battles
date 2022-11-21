using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTarget : AIAction
{
    [SerializeField] private Enemu executor;
    [SerializeField] private Entity target;
    [SerializeField] private float exitDistance;

    [SerializeField] private YG.WeaponData gunData, meleData;

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


        if (executor.WeaponGun)
        {
            YG.WeaponData weaponData = new YG.WeaponData();
            foreach (YG.WeaponData m in LevelManager.Instance.EnemuManager.WeaponDatas)
            {
                if (m.weaponType == executor.WeaponGun.WeaponType)
                {
                    weaponData = m;
                    break;
                }
            }
            gunData = weaponData;
        }
        if (executor.WeaponMele)
        {
            YG.WeaponData weaponData = new YG.WeaponData();
            foreach (YG.WeaponData m in LevelManager.Instance.EnemuManager.WeaponDatas)
            {
                if (m.weaponType == executor.WeaponMele.WeaponType)
                {
                    weaponData = m;
                    break;
                }
            }
            meleData = weaponData;
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


    private void SimulateWeapon(Mele w)
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
        if (attackCount < Mathf.Clamp(meleData.attackCount.Value, 1, 9999))
        {
            if (attackTime >= meleData.attackSpeed.Value)
            {

                executor.Animator.Play("attack");

                attackCount++;
                attackTime = 0;
            }
            attackTime += Time.unscaledDeltaTime;
        }
        else
        {
            if (currentTime >= meleData.reloadSpeed.Value)
            {

                //executor.Animator.Play("reload");

                attackCount = 0;
                currentTime = 0;
            }
            currentTime += Time.unscaledDeltaTime;
        }
    }
    private void SimulateWeapon(Gun w)
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

        if (attackCount < Mathf.Clamp(gunData.patronCount.Value, 1, 9999))
        {
            if (attackTime >= gunData.attackSpeed.Value)
            {

                executor.Animator.Play("attack");

                attackCount++;
                attackTime = 0;
            }
            attackTime += Time.unscaledDeltaTime;
        }
        else
        {
            if (currentTime >= gunData.reloadSpeed.Value)
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
    [SerializeField] float distance;
    public override void CustomUpdate()
    {
        distance = Vector2.Distance(executor.Agent.transform.position, target.Agent.transform.position);
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
        if (executor.WeaponGun && distance <= gunData.attackDistance.Value && distance >= 2f && rotated)
        {
            SimulateWeapon((Gun)executor.WeaponGun);
        }
        // Есть дубинка
        else if (executor.WeaponMele && distance <= executor.Agent.radius + target.Agent.radius + 0.2f && rotated)
        {

            SimulateWeapon((Mele)executor.WeaponMele);
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
