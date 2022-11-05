using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convict : Enemu
{
    [SerializeField] private float exitDistance = 8f;


    [SerializeField] private bool isBegin = false;
    [SerializeField] private bool isIdle = false;
    [SerializeField] private bool isAttack = false;

    public bool IsBegin { get => isBegin; }
    public bool IsAttack { get => isAttack; }
    public bool IsIdle { get => isIdle; }

    protected override void Attacked(Entity attacker)
    {

        if (HitBar.Health <= HitBar.GetMaxHealth() / 2 && !isBegin)
        {
            isBegin = Random.Range(0, 100) >= (100 - LevelManager.Instance.LevelData.BeginChance);
            if (isBegin)
            {
                agent.speed = Mathf.Clamp(agent.speed + 1f, -MoveSpeed.MaxValue, MoveSpeed.MaxValue);
                BeginAction ba = new BeginAction(this, (Enemu)attacker, LevelManager.Instance.GetAreas(AreaType.��������), exitDistance);
                ba.OnComplete?.AddListener((a) =>
                {
                    isBegin = false;
                });
                SetAction(ba);
            }
            return;
        }
        else
        {
            StartAttack(attacker);
        }
    }

    protected override void OnCustomTriggerStay(Entity e)
    {
        if (e != LevelManager.Instance.Player)
        {
            bool agro = Random.Range(0, 100) >= (100 - LevelManager.Instance.LevelData.Pursurehance);
            if (!agro) return;
        }
        if (isBegin) return;
        StartAttack(e);
    }

    protected override void Disable()
    {
        currentAction = null;
        stackActions = new List<AIAction>();
        lifeActions = new List<AIAction>();
        target = null;
        isBegin = false;
        isIdle = false;
        isAttack = false;
    }

    private void SetLife()
    {
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.��������)));
        AddAction(new MoveFromArea(this, LevelManager.Instance.GetAreas(AreaType.��������)));
    }

    public override void Attack()
    {
        if (target && weapon)
        {
            target.TakeDamage(this, weapon.AttackDamage, () =>
            {

            });
            weapon.AnimateAttack();
        }
    }

    protected override void Enabled()
    {
        stackActions = new List<AIAction>();
        lifeActions = new List<AIAction>();
        Gun r = LevelManager.Instance.WeaponManager.GetRandomWeaponByType<Gun>();
        SetGun(r);
        Mele re = LevelManager.Instance.WeaponManager.GetRandomWeaponByType<Mele>();
        SetMele(re);

        SetLife();
    }

    private void StartAttack(Entity e)
    {
        if (isBegin || isIdle || this.target != null || this == e) return;

        if (e as Convict)
        {
            Convict enemu = (Convict)e;
            if (enemu.isBegin || (enemu.Target && (enemu.Target.gameObject.activeSelf && enemu.Target != this)))
            {
                return;
            }
        }

        isAttack = true;
        this.target = e;
        AttackTarget attackAction = new AttackTarget(this, ref target, exitDistance * 2, true);
        attackAction.OnComplete?.AddListener((a) =>
        {
            target = null;
            isAttack = false;
        });
        SetAction(attackAction);
    }

    //protected override void OnCustomTriggerStay(Entity e)
    //{
    //    if (agroBlock) return;

    //    if (e && e as Convict)
    //    {
    //        Convict c = (Convict)e;
    //        ���� � ���� ���� ����
    //        if (c.Target && c.Target.gameObject.activeSelf)
    //        {
    //            return;
    //        }
    //    }
    //    else
    //    {
    //        return;
    //    }
    //    e.TakeDamage(this, 0f, () => { });
    //    StartAttack(e);
    //}
}
